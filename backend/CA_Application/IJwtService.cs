public interface IJwtService
{
    public string GenerateToken(int userId, string username, string role);
    public string GenerateRefreshToken();
    public string HashToken(string token);
}