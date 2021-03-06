﻿using AutoMapper;
using Parking.Core.Models;
using Parking.Core.Models.Data;
using Parking.Database.Entities.Identity;

namespace Parking.Infrastructure.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, AppUser>().ConvertUsing(u => new AppUser { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, PasswordHash = u.PasswordHash });
            CreateMap<AppUser, User>().ConstructUsing(au => new User(au.FirstName, au.LastName, au.Email, au.UserName, au.PasswordHash, au.Id));

            CreateMap<Database.Entities.ParkingLot, ParkingLot>();
            CreateMap<ParkingLot,Database.Entities.ParkingLot>();

            CreateMap<Database.Entities.ParkingSpot, ParkingSpot>();
            CreateMap<ParkingSpot, Database.Entities.ParkingSpot>();

            CreateMap<Database.Entities.ParkingEntry, ParkingEntry>();
            CreateMap<ParkingEntry, Database.Entities.ParkingEntry>();


        }  
    }
}
