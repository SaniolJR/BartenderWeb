using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CA_Application;

internal class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User?> GetByNickAndValidateAsync(LoginRequestDTO request)
    {
        string inputNick = request.Username;
        string inputPasswd = request.Password;
        //get user object from DB
        var userDB = await userRepository.GetByNickAsync(inputNick);

        if (userDB == null)
            return null;

        //verify password
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(userDB, userDB.Passwd, inputPasswd);
        if (result == PasswordVerificationResult.Failed)
            return null;

        return userDB;
    }
}