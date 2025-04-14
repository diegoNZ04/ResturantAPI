using AutoMapper;
using Restaurant.Application.DTOs.Requests.AuthRequests;
using Restaurant.Application.DTOs.Responses.AuthResponses;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, RegisterUserRequest>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
            .ReverseMap();

        CreateMap<User, RegisterUserResponse>()
            .ReverseMap();

        CreateMap<User, LoginUserRequest>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
            .ReverseMap();

        CreateMap<User, LoginUserResponse>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ReverseMap();
    }
}
