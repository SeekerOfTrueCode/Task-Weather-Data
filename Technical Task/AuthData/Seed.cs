using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Technical_Task.AuthData
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            // probably can be replaced with already made class but I don't want to risk that app stops to work after the end refactor xD

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin" };

            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating an admin
            var admin = new IdentityUser
            {
                UserName = config.GetSection("AdminSettings")["UserEmail"],
                Email = config.GetSection("AdminSettings")["UserEmail"]
            };

            var adminPassword = config.GetSection("AdminSettings")["UserPassword"];
            if (await userManager.FindByEmailAsync(config.GetSection("AdminSettings")["UserEmail"]) == null)
            {
                var createPowerUser = await userManager.CreateAsync(admin, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await userManager.AddToRoleAsync(admin, "Admin");

                }
            }

            //creating a standard user
            var standardUser = new IdentityUser
            {
                UserName = config.GetSection("StandardUserSettings")["UserEmail"],
                Email = config.GetSection("StandardUserSettings")["UserEmail"]
            };

            var standardUserPassword = config.GetSection("StandardUserSettings")["UserPassword"];
            if (await userManager.FindByEmailAsync(config.GetSection("StandardUserSettings")["UserEmail"]) == null)
            {
                await userManager.CreateAsync(standardUser, standardUserPassword);
                // no role, because it's standard user
            }
        }
    }
}

