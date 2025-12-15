using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);
            if (result is null|| result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid  refresh token");
            return Ok(result);
        }   
     
        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
                return BadRequest("Invalid  username or password");
          
            return Ok(token);

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {


            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("BadRequest");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpGet("AuthenticateOnlyEndpoint")]
        public ActionResult<string> AuthenticateOnlyEndpoint()
        {
            var userName = User?.Identity?.Name;
            return Ok(userName);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminOlyEndpoint")]
        public ActionResult<string> AdminOlyEndpoint()
        {
            var userName = User?.Identity?.Name;
            return Ok(userName);
        }

    }
}
