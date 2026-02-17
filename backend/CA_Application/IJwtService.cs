public interface IJwtService
{
    public string GenerateToken(int userId, string username, string role);
}