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
        }
    }
}