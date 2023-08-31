using AutoMapper;

using FlightData.BLL.Interfaces;
using FlightData.DAL;
using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

namespace FlightData.BLL.Services
{
    public class FlightService : IFlightServices
    {
        private readonly FlightDataContext _flightDataContext;
        private readonly IMapper _mapper;

        public FlightService(FlightDataContext flightDataContext, IMapper mapper)
        {
            _flightDataContext = flightDataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            var flights = await _flightDataContext.Flights
                .Include(f => f.Airline)
                .ToListAsync();

            return flights;
        }

        public async Task<IEnumerable<Flight>> GetFlightsByCityAsync(int? fromCityId, int? toCityId)
        {
            var query = _flightDataContext.Flights.AsQueryable();

            if (fromCityId == null && toCityId == null)
            {
                throw new ArgumentNullException($"Both value {nameof(fromCityId)} and {nameof(toCityId)} cannot be null at the same time!");
            }

            if (fromCityId != null && toCityId != null)
            {
                query = query.Where(f => f.StartCityId == fromCityId && f.DestinationCityId == toCityId);
            }
            else if (fromCityId == null)
            {
                query = query.Where(f => f.DestinationCityId == toCityId);
            }
            else
            {
                query = query.Where(f => f.StartCityId == toCityId);
            }

            var flights = await query.ToListAsync();

            return flights;
        }
    }
}
