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
            //mapper ignores Ingredients, beacause dto have only names (string)
            CreateMap<AddDrinkDTO, Drink>()
            .ForMember(dest => dest.Ingredients, opt => opt.Ignore());
        }
    }
}