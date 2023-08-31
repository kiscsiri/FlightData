using AutoMapper;

using FlightData.BLL.DTOs;
using FlightData.BLL.Interfaces;
using FlightData.DAL;
using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

namespace FlightData.BLL.Services
{
    public class FlightPlannerService : IFlightPlannerService
    {
        private readonly FlightDataContext _flightDataContext;
        private readonly IMapper _mapper;
        private readonly IFlightServices _flightServices;
        private readonly ICityServices _cityServices;
        private readonly IAirlineServices _airlineServices;

        public FlightPlannerService(FlightDataContext flightDataContext,
            IMapper mapper,
            IFlightServices flightServices,
            ICityServices cityServices,
            IAirlineServices airlineServices)
        {
            _flightDataContext = flightDataContext;
            _mapper = mapper;
            _flightServices = flightServices;
            _cityServices = cityServices;
            _airlineServices = airlineServices;
        }

        public async Task<IEnumerable<FlightDetailsDto>> GetShortestRouteBetweenCitiesAsync(int startCityId, int destinationCityId, bool byAnyAirline)
        {
            var startCity = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                 .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.Airline)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)                
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.Airline)
                .SingleOrDefaultAsync(c => c.Id == startCityId);

            if (startCity == null)
            {
                throw new InvalidOperationException($"Can't find city with Id: [{startCityId}]");
            }

            var destCity = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .SingleOrDefaultAsync(c => c.Id == destinationCityId);

            if (destCity == null)
            {
                throw new InvalidOperationException($"Can't find city with Id: [{destinationCityId}]");
            }

            var allFlights = await _flightServices.GetFlightsAsync();
            var cities = await _cityServices.GetCitiesAsync();
            var airlines = await _airlineServices.GetAirlinesAsync();

            List<FlightDetailsDto> details = new();

            if (!byAnyAirline)
            {
                foreach (var airline in airlines)
                {
                    var result = ConstructFlightRoute(cities.Where(c => c.ArrivalFlights.Any() || c.DepartureFlights.Any()), startCity, destCity, airline);

                    if (result != null)
                    {
                        details.Add(result);
                    }
                }
            }
            else
            {
                var result = ConstructFlightRoute(cities.Where(c => c.ArrivalFlights.Any() || c.DepartureFlights.Any()), startCity, destCity, null);

                if (result != null)
                {
                    details.Add(result);
                }
            }

            return details;
        }

        private FlightDetailsDto ConstructFlightRoute(IEnumerable<City> cities, City startCity, City destCity, Airline? airline)
        {
            var currentCity = startCity;
            var visitedCityIds = new HashSet<int>();
            var unvisitedCities = cities.ToHashSet();

            // Key marks the city id, and the value is the shortest path generated from startCity
            IDictionary<int, FlightDetailsDto> shortestFlightRoutesFromStart = new Dictionary<int, FlightDetailsDto>()
            {
                { startCity.Id, new FlightDetailsDto { Airline = airline} }
            };

            FlightDetailsDto? currentCityShortestFlightDetails = new();

            while (unvisitedCities.Any())
            {
                // Check the current lowest distance city from the start city, then remove it from unvisited cities
                var lowestDistanceCity = shortestFlightRoutesFromStart
                    .Where(r => unvisitedCities.Select(c => c.Id).ToList().Contains(r.Key))
                    .OrderBy(r => r.Value.TotalDistance)
                    .FirstOrDefault();

                currentCity = unvisitedCities.FirstOrDefault(c => c.Id == lowestDistanceCity.Key);

                if (currentCity == null)
                {
                    break;
                }

                unvisitedCities.Remove(currentCity);

                currentCityShortestFlightDetails = lowestDistanceCity.Value;
                var lastFlightToCurrentCity = currentCityShortestFlightDetails.Flights.LastOrDefault();

                // Get the list of unvisited city flight routes from the current city, ordered by distance,
                // filtered to only get the flights in the future + 1 hour delay for the transfer
                var unvisitedCityFlightsFromCurrentCity = currentCity.DepartureFlights
                    .Where(df => unvisitedCities.Contains(df.DestinationCity) && df.TakeOffDate > (lastFlightToCurrentCity?.ArrivalDate.AddHours(1) ?? DateTime.Now))
                    .OrderBy(d => d.Distance)
                    .ToList();

                if (airline != null)
                {
                    unvisitedCityFlightsFromCurrentCity = unvisitedCityFlightsFromCurrentCity.Where(f => f.AirlineId == airline.Id).ToList();
                }

                if (unvisitedCityFlightsFromCurrentCity != null && unvisitedCityFlightsFromCurrentCity.Any())
                {
                    foreach (var unvisitedCityFlight in unvisitedCityFlightsFromCurrentCity)
                    {
                        var currentShortestDistanceToCity = shortestFlightRoutesFromStart.TryGetValue(unvisitedCityFlight.DestinationCityId, out var shortestDistanceRoute);
                        var newTotalDistance = currentCityShortestFlightDetails.TotalDistance + unvisitedCityFlight.Distance;

                        if ((shortestDistanceRoute != null && newTotalDistance < shortestDistanceRoute.TotalDistance) || !currentShortestDistanceToCity)
                        {
                            var flightPlan = new FlightDetailsDto
                            {
                                Flights = currentCityShortestFlightDetails.Flights.ToList(),
                                Airline = airline
                            };

                            flightPlan.Flights.Add(unvisitedCityFlight);
                            shortestFlightRoutesFromStart[unvisitedCityFlight.DestinationCityId] = flightPlan;
                        }
                    }
                }
            }

            return shortestFlightRoutesFromStart.TryGetValue(destCity.Id, out var result) ? result : new FlightDetailsDto { Airline = airline };
        }
    }
}
