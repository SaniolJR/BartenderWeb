using AutoMapper;
using CA_Domain.Entities;
using CA_Application.DTOs;

namespace CA_Application.DTOs
{
    public class DrinkProfile : Profile
    {
        //For adding drink
        public DrinkProfile()
        {
            CreateMap<AddDrinkDTO, Drink>();
        }
    }
}