using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using CA_Application.DTOs;
using CA_Application;

namespace Endpoints.Auth;

[ApiController]
[Route("api/auth")]
public class AuthEndpoints(IJwtService jwtService, IUserService userService, IRefreshTokenService refreshTokenService) : ControllerBase
{

    private async Task GenerateTokens(int id, string nick, string role)
    {
        //generate JWT token
        var token = jwtService.GenerateToken(id, nick, role);
        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });



        var refreshToken = await refreshTokenService.GenerateAndSaveAsync(id);
        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLogin([FromBody] LoginRequestDTO request)
    {
        //if exists - generate JWT, pack into cookie and response
        var user = await userService.GetByNickAndValidateAsync(request);
        if (user != null)
        {
            //generate JWT and reftesh tokens
            await GenerateTokens(user.Id, user.Username, user.Role);

            return Ok(user);
        }
        else        //if user doesnt exist
        {
            return Unauthorized(new { message = "Unvalid login data!" });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> UserLogout()
    {

        var cookiesToken = Request.Cookies["RefreshToken"];
        if (cookiesToken != null)
        {
            var dbToken = await refreshTokenService.ValidateAsync(cookiesToken);
            if (dbToken != null)
            {
                await refreshTokenService.RevokeAsync(dbToken);
            }
        }
        Response.Cookies.Delete("AuthToken");
        Response.Cookies.Delete("RefreshToken");

        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshSession()
    {
        var cookiesToken = Request.Cookies["RefreshToken"];

        if (cookiesToken == null)
            return Unauthorized(new { message = "Session has been expired" });

        var dbToken = await refreshTokenService.ValidateAsync(cookiesToken);

        if (dbToken == null)
            return Unauthorized(new { message = "Session has been expired" });

        await refreshTokenService.RevokeAsync(dbToken);

        //generate JWT and reftesh tokens
        await GenerateTokens(dbToken.UserObj.Id, dbToken.UserObj.Username, dbToken.UserObj.Role);

        return Ok();
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccDTO request)
    {

        //check if user exists
        var seachedUser = await userService.GetByNickAsync(request.Username);
        if (seachedUser != null)
        {
            return Conflict("User already exists");
        }

        //create account
        var user = await userService.CreateAccount(request);
        return Ok(user);

    }
}