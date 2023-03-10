using AutoMapper;
using flightFinder.API.DTOs.Outgoing;
using flightFinder.API.Models;

namespace flightFinder.API.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FlightRoute, FlightRouteDto>();
        CreateMap<Flight, FlightDto>();
        CreateMap<Price, PriceDto>();
        CreateMap<Flight, FlightWithLayoverDto>()
            .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => $"{src.FlightId}"))
            .ForMember(dest => dest.DepartureAt, opt => opt.MapFrom(src => src.DepartureAt))
            .ForMember(dest => dest.ArrivalAt, opt => opt.MapFrom(src => src.ArrivalAt))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableSeats))
           //.ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.Prices))
            .ForMember(dest => dest.FlightRoute, opt => opt.MapFrom(src => src.FlightRoute));
        // .ForMember(dest => dest.LayoverDestination, opt => opt.MapFrom(src => src.FlightRoute.LayoverDestination))
        // .ForMember(dest => dest.LayoverDuration, opt => opt.MapFrom(src => src.FlightRoute.LayoverDuration));
        
    }
}
  