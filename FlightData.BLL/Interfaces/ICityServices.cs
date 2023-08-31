using FlightData.BLL.DTOs;
using FlightData.Model.Entities;

namespace FlightData.BLL.Interfaces
{
    public interface ICityServices
    {
        Task<City> GetSmallestCityAsync();
        Task<City> GetBiggestCityAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
    }
}
