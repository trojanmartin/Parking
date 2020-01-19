using Parking.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Extensions
{
    public static class UserExtensions
    {
        public static User WithoutPassword(this User user)
        {
            return new User(user.FirstName, user.LastName, user.Email, user.UserName,null,user.Id);
        }
    }
}
