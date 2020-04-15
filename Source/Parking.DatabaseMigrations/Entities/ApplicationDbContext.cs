﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.Database.Entities;
using Parking.Database.Entities.Identity;

namespace Parking.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkEntry> ParkEntries { get; set; }
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

            builder.Entity<ParkingLot>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();


            builder.Entity<ParkEntry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            builder.Entity<ParkEntry>()
              .Property(e => e.Id)
              .IsRequired();

            builder.Entity<ParkingSpot>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Sensor>()
            .HasKey(x => x.Devui);


            base.OnModelCreating(builder);
        }
    }
}
