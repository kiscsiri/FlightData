using AutoMapper;

using FlightData.BLL.DTOs;
using FlightData.BLL.Interfaces;
using FlightData.DAL;
using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.Services
{
    public class FlightPlannerService : IFlightPlannerService
    {
        private readonly FlightDataContext _flightDataContext;
        private readonly IMapper _mapper;
        private readonly IFlightServices _flightServices;

        public FlightPlannerService(FlightDataContext flightDataContext,
            IMapper mapper,
            IFlightServices flightServices)
        {
            _flightDataContext = flightDataContext;
            _mapper = mapper;
            _flightServices = flightServices;
        }

        public async Task<FlightDetailsDto> GetShortestRouteBetweenCitiesAsync(int startCityId, int destinationCityId)
        {
            var startCity = await _flightDataContext.Cities.SingleOrDefaultAsync(c => c.Id == startCityId);

            if (startCity == null)
            {
                throw new InvalidOperationException($"Can't find city with Id: [{startCityId}]");
            }

            var destCity = await _flightDataContext.Cities.SingleOrDefaultAsync(c => c.Id == destinationCityId);

            if (destCity == null)
            {
                throw new InvalidOperationException($"Can't find city with Id: [{destinationCityId}]");
            }

            var flights = await _flightServices.GetFlightsByCityAsync(startCityId, destinationCityId);

            if (flights.Any())
            {
                var shortestRoute = flights.OrderBy(f => f.Distance).FirstOrDefault();

                return new FlightDetailsDto
                {
                    Airline = shortestRoute.Airline,
                    Flights = new List<GetFlightsDto>() { shortestRoute },
                    TotalTime = TimeHelpers.CalculateDifference(shortestRoute.TakeOffDate, shortestRoute.ArrivalDate)
                };
            }
            else
            {
                return await ConstructFlightRoute(startCity, destCity);
            }
        }

        private async Task<FlightDetailsDto> ConstructFlightRoute(City startCity, City destCity)
        {
            var startFlights = await _flightServices.GetFlightsByCityAsync(startCity.Id, null);

            foreach (var flight in startFlights.OrderBy(f => f.Distance))
            {
                
            }

            return new FlightDetailsDto();
        }
    }
}
