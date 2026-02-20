using CA_Domain.Entities;
using CA_Domain.Repositories;

namespace CA_Application
{
    internal class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
         IJwtService jwtService) : IRefreshTokenService
    {
        public async Task<string> GenerateAndSaveAsync(int userId)
        {
            string token = jwtService.GenerateRefreshToken();
            string hashedToken = jwtService.HashToken(token);
            await refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = hashedToken,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });
            return token;
        }
        public async Task<RefreshToken?> ValidateAsync(string token)
        {
            string hashedToken = jwtService.HashToken(token);
            var tokenFromDb = await refreshTokenRepository.GetByTokenAsync(hashedToken);

            //return tuken if it isn't revoked and is valid
            if (tokenFromDb == null || tokenFromDb.IsRevoked || tokenFromDb.ExpiresAt < DateTime.UtcNow)
                return null;

            return tokenFromDb;
        }

        public async Task RevokeAsync(RefreshToken token)
        {
            await refreshTokenRepository.RevokeAsync(token);
        }
    }
}