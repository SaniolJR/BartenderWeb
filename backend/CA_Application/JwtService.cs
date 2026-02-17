using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{

    private readonly string key;
    private readonly string issuer;
    private readonly string audience;

    public JwtService(IConfiguration config)
    {
        // get form appsettings.json:
        key = config["Jwt:Key"];
        issuer = config["Jwt:Issuer"];
        audience = config["Jwt:Audience"];
    }
    public string GenerateToken(int userId, string username, string role)
    {
        var handler = new JwtSecurityTokenHandler();

        var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

        //configure infos about user
        var claims = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)    //admin or user
        });

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = claims,   //userdata
            Expires = DateTime.UtcNow.AddMinutes(5),    //expires every 5 min
            Issuer = issuer,       //WebServer sends
            Audience = audience,        //WebApp gets it
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
        };

        //create JWT token
        var token = handler.CreateToken(descriptor);
        //write and return
        return handler.WriteToken(token);
    }
}