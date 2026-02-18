using AutoMapper;
using CA_Domain.Entities;
using CA_Application.DTOs;

namespace CA_Application.DTOs
{
    public class UserProfile : Profile
    {
        //For returning user after login
        public UserProfile()
        {
            CreateMap<User, UserReturnDTO>();
            CreateMap<UserReturnDTO, User>();
            CreateMap<RegisterAccDTO, User>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => "user"));
        }
    }
}