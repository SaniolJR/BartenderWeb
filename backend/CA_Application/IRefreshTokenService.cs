using CA_Domain.Entities;

namespace CA_Application
{
    public interface IRefreshTokenService
    {
        public Task<string> GenerateAndSaveAsync(int userId);
        public Task<RefreshToken?> ValidateAsync(string token);
        public Task RevokeAsync(RefreshToken token);
    }
}