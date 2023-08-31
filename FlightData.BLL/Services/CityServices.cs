using AutoMapper;

using FlightData.BLL.Interfaces;
using FlightData.DAL;
using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

namespace FlightData.BLL.Services
{
    public class CityServices : ICityServices
    {
        private readonly FlightDataContext _flightDataContext;
        private readonly IMapper _mapper;

        public CityServices(FlightDataContext flightDataContext, IMapper mapper)
        {
            _flightDataContext = flightDataContext;
            _mapper = mapper;
        }

        public async Task<City> GetBiggestCityAsync()
        {
            var city = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .OrderByDescending(x => x.Population)
                .FirstOrDefaultAsync();

            return city;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.Airline)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.Airline)
                .ToListAsync();
        }

        public async Task<City> GetSmallestCityAsync()
        {
            var city = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .OrderBy(x => x.Population)
                .FirstOrDefaultAsync();

            return city;
        }
    }
}
