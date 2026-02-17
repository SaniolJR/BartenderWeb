using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using CA_Application.DTOs;

namespace Endpoints.Auth;

[ApiController]
[Route("api/auth")]
public class AuthEndpoints(IJwtService jwtService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> UserLogin([FromBody] LoginRequestDTO request)
    {
        //if exists - generate JWT, pack into cookie and response
        if (request.Username == "user" && request.Password == "passwd")
        {
            int id = 1;
            var token = jwtService.GenerateToken(id, request.Username, "user");
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)  //cookie is invalit after 7 days
            });
            return Ok(new { token });
        }
        else        //if user doesnt exist
        {
            return Unauthorized(new { message = "Unvalid login data!" });
        }
    }

    [HttpPost("logout")]
    public IActionResult UserLogout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok();
    }
}