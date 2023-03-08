using AutoMapper;
using flightFinder.API.DTOs.Outgoing;
using flightFinder.API.Models;

namespace flightFinder.API.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FlightRoute, FlightRouteDto>()
            .ForMember(dest => dest.RouteId, opt => opt.MapFrom(src => src.RouteId))
            .ForMember(dest => dest.DepartureDestination, opt => opt.MapFrom(src => src.DepartureDestination))
            .ForMember(dest => dest.ArrivalDestination, opt => opt.MapFrom(src => src.ArrivalDestination))
            .ForMember(dest => dest.Itineraries, opt => opt.MapFrom(src => src.Itineraries));
        CreateMap<Flight, FlightDto>()
            .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => src.FlightId))
            .ForMember(dest => dest.DepartureAt, opt => opt.MapFrom(src => src.DepartureAt))
            .ForMember(dest => dest.ArrivalAt, opt => opt.MapFrom(src => src.ArrivalAt))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableSeats))
            .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.Prices));
        CreateMap<Price, PriceDto>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.Adult, opt => opt.MapFrom(src => src.Adult))
            .ForMember(dest => dest.Child, opt => opt.MapFrom(src => src.Child));
    }
}