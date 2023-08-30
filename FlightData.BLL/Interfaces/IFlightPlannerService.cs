using FlightData.BLL.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.Interfaces
{
    public interface IFlightPlannerService
    {
        Task<FlightDetailsDto> GetShortestRouteBetweenCitiesAsync(int startCityId, int destinationCityId);
    }
}
