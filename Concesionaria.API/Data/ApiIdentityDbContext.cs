using Concesionaria.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Concesionaria.API.Data
{
    public class ApiIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApiIdentityDbContext(DbContextOptions<ApiIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);         
        }
    }
}
