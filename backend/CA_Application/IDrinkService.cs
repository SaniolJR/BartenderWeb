using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;

namespace CA_Application
{
    public interface IDrinkService
    {
        Task<Drink> AddDrinkAsync(AddDrinkDTO dto);
    }
}