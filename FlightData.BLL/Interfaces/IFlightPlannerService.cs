using FlightData.BLL.DTOs;

namespace FlightData.BLL.Interfaces
{
    public interface IFlightPlannerService
    {
        Task<IEnumerable<FlightDetails>> GetShortestRouteBetweenCitiesAsync(int startCityId, int destinationCityId, bool byAnyAirline);
    }
}
