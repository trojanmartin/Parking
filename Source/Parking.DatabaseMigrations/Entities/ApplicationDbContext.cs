using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.Database.Entities;
using Parking.Database.Entities.Identity;

namespace Parking.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingEntry> ParkEntries { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<AppUser>()
                   .Property(e => e.Id)
                   .ValueGeneratedOnAdd();
           
            builder.Entity<ParkingLot>(options =>
            {
                options.Property(e => e.Id)
                       .ValueGeneratedOnAdd();
            });


            builder.Entity<ParkingSpot>(options =>
            {
                options.HasKey(x => new { x.Name, x.ParkingLotId });
            });

            builder.Entity<ParkingEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<ParkingEntry>()
              .Property(e => e.Id)
              .IsRequired();               

            builder.Entity<Sensor>()
            .HasKey(x => x.Devui);


            base.OnModelCreating(builder);
        }
    }
}
