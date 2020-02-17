using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.Infrastructure.Identity;

namespace Parking.Infrastructure.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {          

            builder.Entity<AppUser>()
                   .Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}
