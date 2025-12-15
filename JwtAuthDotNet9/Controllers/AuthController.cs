using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
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
        public static User user = new User();
        [HttpPost("Login")]
        public ActionResult<string> Login(UserDto request)
        {
           var token= authService.LoginAsync( request);
            return Ok(token);

        }
        [HttpPost("Register")] 
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            
          
            var user= await authService.RegisterAsync(request );
            if(user is null)
            {
            return BadRequest("BadRequest");
            }
            return Ok(user);
        }

      
    }
}
