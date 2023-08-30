using FlightData.BLL.DTOs;

namespace FlightData.BLL.Interfaces
{
    public interface ICityServices
    {
        Task<GetCitiesDto> GetSmallestCityAsync();
        Task<GetCitiesDto> GetBiggestCityAsync();
    }
}
