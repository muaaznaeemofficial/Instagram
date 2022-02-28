using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.SeedData
{
    public static class DefaultUsers
    {
        public static IEnumerable<AppUser> Users()
        {
            var users = new List<AppUser>();


            for (int i = 0; i < 10; i++)
            {
                var user = new AppUser()
                {
                    PhoneNumber = "+098765433",
                    UserName = $"user{i}@g.com",
                    Email = $"user{i}@g.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                users.Add(user);
            }
            return users;
        }


        public static AppUser AdminUser()
        {
            var admin = new AppUser
            {

                PhoneNumber = "+098765433",
                UserName = "ad@g.com",
                Email = "ad@g.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            return admin;
        }
    }
}
