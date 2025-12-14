using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new User();
        [HttpPost("register")]
        public ActionResult<User> Login(UserDto request)
        {
           var hashedPassword=new PasswordHasher<User>().HashPassword(user, request.PasswordHash);
            user.UserName=request.UserName;
            user.PasswordHash=hashedPassword;
            return Ok(user);

        }
        [HttpPost("login")] 
        public ActionResult<string> Register(UserDto request)
        {
            
            var passwordVerificationResult=new PasswordHasher<User>().VerifyHashedPassword(user,user.PasswordHash,request.PasswordHash);
            if(passwordVerificationResult==PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password");
            }
            string token = CraeteToken(user);
            return Ok(token);
        }

        private string CraeteToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
               configuration.GetValue<string>("AppSettings:Token")!
            ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"), 
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(tokendescriptor);
        }
    }
}
