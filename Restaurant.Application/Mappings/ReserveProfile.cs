using AutoMapper;
using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.DTOs.Responses.ReservesResponses;
using Restaurant.Application.Helpers;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappings;

public class ReserveProfile : Profile
{
    public ReserveProfile()
    {
        CreateMap<Reserve, CreateReserveRequest>()
            .ReverseMap();

        CreateMap<Reserve, UpdateReserveRequest>()
            .ReverseMap();

        CreateMap<Reserve, UpdateReserveStatusRequest>()
            .ReverseMap();

        CreateMap<Reserve, CreateReserveResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ReserveDate, opt => opt.MapFrom(src => DateTimeHelper.ToBrasiliaTime(src.ReserveDate)))
            .ReverseMap();

        CreateMap<Reserve, UpdateReserveResponse>()
            .ForMember(dest => dest.ReserveDate, opt => opt.MapFrom(src => DateTimeHelper.ToBrasiliaTime(src.ReserveDate)))
            .ReverseMap();

        CreateMap<Reserve, UpdateReserveStatusResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Reserve, GetAllReservesResponse>()
            .ForMember(dest => dest.ReserveDate, opt => opt.MapFrom(src => DateTimeHelper.ToBrasiliaTime(src.ReserveDate)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Reserve, GetReserveByIdResponse>()
            .ForMember(dest => dest.ReserveDate, opt => opt.MapFrom(src => DateTimeHelper.ToBrasiliaTime(src.ReserveDate)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
