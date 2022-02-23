
using Backend.Constants;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class Seeder
    {
        public async static Task SeedUsers(IServiceProvider services)
        {

            var _context = services.GetRequiredService<AppDbContext>();
            var _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = services.GetRequiredService<UserManager<AppUser>>();


            //Admin
            var admin = new AppUser
            {

                PhoneNumber = "+098765433",
                UserName = "ad@g.com",
                Email = "ad@g.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Roles.Any(r => r.Name == AppConstants.RoleAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = AppConstants.RoleAdmin });
            }

            if (!_context.Users.Any(u => u.UserName == admin.UserName))
            {
                await _userManager.CreateAsync(admin, "Ad@123");
                await _userManager.AddToRoleAsync(admin, AppConstants.RoleAdmin);
            }

            await _context.SaveChangesAsync();
        }



    }
}
