using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Exceptions;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Models.BaseModels;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDotNet9.Controllers
{
    
   
    public class AuthController(IAuthService authService) : BaseController
    {

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);
            if (result is null|| result.AccessToken is null || result.RefreshToken is null)
                 throw new BusinessException("","Invalid  refresh token"); 
            return Success<TokenResponseDto?>(result);
        }   
     
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
                 throw new BusinessException("","Invalid  username or password"); 
          
            return Success< TokenResponseDto>(token);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto request)
        {


            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                throw new BusinessException("", "BadRequest");
               
            }
            return Success(user);
        }
        [Authorize]
        [HttpGet("AuthenticateOnlyEndpoint")]
        public IActionResult AuthenticateOnlyEndpoint()
        {
            var userName = User?.Identity?.Name;
            return Success(userName);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminOlyEndpoint")]
        public IActionResult AdminOlyEndpoint()
        {
            var userName = User?.Identity?.Name;
            return Success<string?>(userName);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 1)
                throw new BusinessException("", "Admin silinemez");

            return Success("Silindi");
        }

    }
}
