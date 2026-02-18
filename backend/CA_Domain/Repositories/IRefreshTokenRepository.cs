using CA_Domain.Entities;

namespace CA_Domain.Repositories;

public interface IRefreshTokenRepository
{
    public Task AddAsync(RefreshToken token);
    public Task<RefreshToken?> GetByTokenAsync(string token);
    public Task RevokeAsync(RefreshToken token);
}