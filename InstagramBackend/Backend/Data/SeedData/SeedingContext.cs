using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.SeedData
{
    public static class SeedingContext
    {
        public static IEnumerable<AppUser> GetUsers()
        {
            return DefaultUsers.Users();
        }

    }
}
