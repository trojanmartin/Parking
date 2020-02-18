using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Infrastructure.Data.Entities;

namespace Parking.Mqtt.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MqttServerConfiguration> MqttServerConfigurations { get; set; }

        public DbSet<MqttTopicConfiguration> MqttTopicConfigurations { get; set; }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MqttServerConfiguration>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MqttTopicConfiguration>()
              .Property(e => e.Id)
              .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

    }
}
