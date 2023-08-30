using AutoMapper;

using FlightData.BLL.DTOs;
using FlightData.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.MapperProfiles
{
    public class FlightDataProfile : Profile
    {
        public FlightDataProfile()
        {
            CreateMap<Airline, GetAirlinesDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Flights, opt => opt.MapFrom(src => src.Flights))
                .ReverseMap();

            CreateMap<Flight, GetFlightsDto>()
                .ForMember(dst => dst.Airline, opt => opt.MapFrom(src => src.Airline))
                .ForMember(dst => dst.DestinationCity, opt => opt.MapFrom(src => src.DestinationCity))
                .ForMember(dst => dst.StartCity, opt => opt.MapFrom(src => src.StartCity))
                .ForMember(dst => dst.TakeOffDate, opt => opt.MapFrom(src => src.TakeOffDate))
                .ForMember(dst => dst.Distance, opt => opt.MapFrom(src => src.Distance))
                .ForMember(dst => dst.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ReverseMap();

            CreateMap<City, GetCitiesDto>()
                .ForMember(dst => dst.CityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Population, opt => opt.MapFrom(src => src.Population))
                .ForMember(dst => dst.ArrivalFlights, opt => opt.MapFrom(src => src.ArrivalFlights))
                .ForMember(dst => dst.DepartureFlights, opt => opt.MapFrom(src => src.DepartureFlights))
                .ReverseMap();
        }
    }
}
