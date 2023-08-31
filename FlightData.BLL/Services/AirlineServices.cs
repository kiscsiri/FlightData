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
    public class AirlineServices : IAirlineServices
    {
        private readonly FlightDataContext _flightDataContext;
        private readonly IMapper _mapper;

        public AirlineServices(FlightDataContext flightDataContext, IMapper mapper)
        {
            _flightDataContext = flightDataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Airline>> GetAirlinesAsync()
        {
            var airlines = await _flightDataContext.Airlines.ToListAsync();
            
            return airlines;
        }

        public async Task<IEnumerable<GetAirlinesDto>> GetFlightsForAirlineAsync(int airlineId)
        {
            var airlines = await _flightDataContext.Airlines.Where(a => a.Id == airlineId)
                .Include(a => a.Flights)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetAirlinesDto>>(airlines);
        }
    }
}
