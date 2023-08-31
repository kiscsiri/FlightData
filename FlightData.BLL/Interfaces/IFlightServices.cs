using FlightData.Model.Entities;

namespace FlightData.BLL.Interfaces
{
    public interface IFlightServices
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<IEnumerable<Flight>> GetFlightsByCityAsync(int? fromCityId, int? toCityId);
    }
}
