using AutoMapper;
using RestaurantAPI.DTOs;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Utilities
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Reserve, ReserveDTO>();
            CreateMap<Reserve, ReserveDetailsDTO>();
            CreateMap<Table, TableDTO>();
        }
    }
}