using AutoMapper;

using FlightData.BLL.DTOs;
using FlightData.BLL.Interfaces;
using FlightData.DAL;

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

        public async Task<GetCitiesDto> GetBiggestCityAsync()
        {
            var city = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .OrderByDescending(x => x.Population)
                .FirstOrDefaultAsync();

            return _mapper.Map<GetCitiesDto>(city);
        }

        public async Task<GetCitiesDto> GetSmallestCityAsync()
        {
            var city = await _flightDataContext.Cities
                .Include(c => c.ArrivalFlights)
                    .ThenInclude(f => f.StartCity)
                .Include(c => c.DepartureFlights)
                    .ThenInclude(f => f.DestinationCity)
                .OrderBy(x => x.Population)
                .FirstOrDefaultAsync();

            return _mapper.Map<GetCitiesDto>(city);
        }
    }
}
