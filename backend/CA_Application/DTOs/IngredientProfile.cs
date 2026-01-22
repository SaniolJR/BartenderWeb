using AutoMapper;
using CA_Domain.Entities;

namespace CA_Application.DTOs
{
    public class IngredientProfile : Profile
    {
        //For adding drink
        public IngredientProfile()
        {
            CreateMap<AddIngredientDTO, Ingredient>();
        }
    }
}
