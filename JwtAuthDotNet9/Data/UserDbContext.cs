using JwtAuthDotNet9.Entites;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthDotNet9.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) :DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
