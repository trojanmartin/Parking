﻿using Microsoft.AspNetCore.Identity;

namespace Parking.Database.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
