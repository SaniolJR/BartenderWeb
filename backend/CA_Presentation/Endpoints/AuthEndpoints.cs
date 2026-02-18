using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using CA_Application.DTOs;
using CA_Application;
using CA_Infrastructure.Repositories;

namespace Endpoints.Auth;

[ApiController]
[Route("api/auth")]
public class AuthEndpoints(IJwtService jwtService, IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> UserLogin([FromBody] LoginRequestDTO request)
    {
        //if exists - generate JWT, pack into cookie and response
        var user = await userService.GetByNickAndValidateAsync(request);
        if (user != null)
        {
            var token = jwtService.GenerateToken(user.Id, user.Nick, user.Role);
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(5)  //cookie is invalit after 7 days - change
            });
            return Ok(user);
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
    //todo endpoint odswierzajacy <5min
}