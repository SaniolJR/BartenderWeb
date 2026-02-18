using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;

namespace CA_Application;

public interface IUserService
{
    Task<UserReturnDTO?> GetByNickAndValidateAsync(LoginRequestDTO request);

    Task<UserReturnDTO?> GetByNickAsync(string nick);

    Task<UserReturnDTO> CreateAccount(RegisterAccDTO request);
}