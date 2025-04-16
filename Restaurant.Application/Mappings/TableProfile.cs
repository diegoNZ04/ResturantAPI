using AutoMapper;
using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.DTOs.Responses.TablesResponses;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappings;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, CreateTableRequest>()
            .ReverseMap();

        CreateMap<Table, UpdateTableRequest>()
            .ReverseMap();

        CreateMap<Table, CreateTableResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();

        CreateMap<Table, UpdateTableResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();

        CreateMap<Table, GetAllTablesResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Table, GetTableByIdResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
