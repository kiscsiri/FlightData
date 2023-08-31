using FlightData.BLL.DTOs;
using FlightData.Model.Entities;

namespace FlightData.BLL.Interfaces
{
    public interface IAirlineServices
    {
        Task<IEnumerable<Airline>> GetAirlinesAsync();
        Task<IEnumerable<GetAirlinesDto>> GetFlightsForAirlineAsync(int id);
    }
}
