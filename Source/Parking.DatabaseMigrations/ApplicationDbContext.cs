using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.Database.Entities;
using Parking.Database.Entities.Identity;

namespace Parking.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<MqttServerConfiguration> MqttServerConfigurations { get; set; }

        public DbSet<MqttTopicConfiguration> MqttTopicConfigurations { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AppUser>()
                   .Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Entity<MqttServerConfiguration>()
               .Property(e => e.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<MqttTopicConfiguration>()
              .Property(e => e.Id)
              .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}
