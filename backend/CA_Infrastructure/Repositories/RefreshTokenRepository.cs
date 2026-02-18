using CA_Domain.Entities;
using CA_Domain.Repositories;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CA_Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{

    private readonly MainDbContext _dbContext;

    public RefreshTokenRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _dbContext.RefreshTokens.Add(token);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _dbContext.RefreshTokens
                        .Include(t => t.UserObj)
                        .FirstOrDefaultAsync(t => t.Token == token);
    }
    public async Task RevokeAsync(RefreshToken token)
    {
        token.IsRevoked = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ChangeUserPassword(User user, string newPassword)
    {
        try
        {
            user.Password = newPassword;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}