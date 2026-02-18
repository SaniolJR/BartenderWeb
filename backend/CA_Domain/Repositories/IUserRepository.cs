using CA_Domain.Entities;

namespace CA_Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByNickAsync(string nick);
    Task<User> CreateUserAsync(User user);
    public Task<bool> ChangeUserPassword(User user, string newPassword);
}