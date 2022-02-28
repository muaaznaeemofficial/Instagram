
using Backend.Constants;
using Backend.Data.SeedData;
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
            var adminUser = DefaultUsers.AdminUser();

            //Admin role
            if (!_context.Roles.Any(r => r.Name == AppConstants.RoleAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = AppConstants.RoleAdmin });
            }

            //Admin user
            if (!_context.Users.Any(u => u.UserName == adminUser.UserName))
            {
                await _userManager.CreateAsync(adminUser, PasswordConstants.AdminPassword);
                await _userManager.AddToRoleAsync(adminUser, AppConstants.RoleAdmin);
            }


            //User Role
            if (!_context.Roles.Any(r => r.Name == AppConstants.RoleUser))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = AppConstants.RoleUser });
            }

            //Users
            foreach (var user in SeedingContext.GetUsers())
            {
                if (!_context.Users.Any(u => u.UserName == user.UserName))
                {
                    await _userManager.CreateAsync(user, PasswordConstants.UserPassword);
                    await _userManager.AddToRoleAsync(user, AppConstants.RoleUser);
                }
            }

            await _context.SaveChangesAsync();
        }



    }
}
