using CA_Domain.Entities;
using CA_Domain.Repositories;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CA_Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MainDbContext _dbContext;

    public UserRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByNickAsync(string nick)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Nick == nick);
    }
}