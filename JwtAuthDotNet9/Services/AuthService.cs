using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtAuthDotNet9.Data;
using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDotNet9.Services
{
    public class AuthService(UserDbContext context,IConfiguration configuration) : IAuthService
    {
        public async Task<string?> LoginAsync(UserDto request)
        {
            var user=await context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if(user == null)
            {
                return null;
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.PasswordHash) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return CraeteToken(user);

        }

        private string CraeteToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

        public async Task<User?> RegisterAsync(UserDto request)
        {
            if(await context.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                return null;
            }
            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.PasswordHash);
            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;
            context.Users.Add(user);
            await  context.SaveChangesAsync();
            return user;
        }
    }
}
