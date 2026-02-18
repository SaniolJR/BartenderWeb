using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CA_Application;

internal class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<UserReturnDTO?> GetByNickAndValidateAsync(LoginRequestDTO request)
    {
        string inputNick = request.Username;
        string inputPasswd = request.Password;
        //get user object from DB
        var userDB = await userRepository.GetByNickAsync(inputNick);

        if (userDB == null)
            return null;

        //verify password
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(userDB, userDB.Password, inputPasswd);
        if (result == PasswordVerificationResult.Failed)
            return null;

        return mapper.Map<UserReturnDTO>(userDB);
    }

    public async Task<UserReturnDTO?> GetByNickAsync(string nick)
    {
        var userDB = await userRepository.GetByNickAsync(nick);
        if (userDB == null)
            return null;
        return mapper.Map<UserReturnDTO>(userDB);
    }

    public async Task<UserReturnDTO> CreateAccount(RegisterAccDTO request)
    {
        string plainPasswd = request.Password;

        //hash password
        var hasher = new PasswordHasher<User>();
        string hashedPassword = hasher.HashPassword(null, plainPasswd);
        request.Password = hashedPassword;

        //map DTO to User class
        var user = mapper.Map<User>(request);

        //create user
        var result = await userRepository.CreateUserAsync(user);

        //return dto without hashed password
        return mapper.Map<UserReturnDTO>(result);
    }
}