using FlightData.BLL.DTOs;
using FlightData.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.Interfaces
{
    public interface IFlightServices
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<IEnumerable<Flight>> GetFlightsByCityAsync(int? fromCityId, int? toCityId);
    }
}
