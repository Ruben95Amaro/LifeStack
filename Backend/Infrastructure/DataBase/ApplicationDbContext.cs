using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserRoles> Roles { get; set; }
    }
}
