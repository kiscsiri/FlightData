using FlightData.BLL.DTOs;
using FlightData.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.Interfaces
{
    public interface IFlightPlannerService
    {
        Task<IEnumerable<FlightDetailsDto>> GetShortestRouteBetweenCitiesAsync(int startCityId, int destinationCityId, bool byAnyAirline);
    }
}
