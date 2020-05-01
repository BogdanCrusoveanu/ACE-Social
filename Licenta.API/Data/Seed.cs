using System.Collections.Generic;
using System.Linq;
using Licenta.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Licenta.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userNumber = 0;
                var userData = System.IO.File.ReadAllText("Data/UsersData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Profesor"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Student"},
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    userNumber++;
                    if (userNumber < 12)
                    {
                        userManager.CreateAsync(user, "Corona").Wait();
                        userManager.AddToRoleAsync(user, "Student").Wait();
                    } else
                    {
                        userManager.CreateAsync(user, "Corona").Wait();
                        userManager.AddToRoleAsync(user, "Profesor").Wait();
                    }
                }

                var adminUser = new User
                {
                    UserName = "Admin"
                };

                IdentityResult result = userManager.CreateAsync(adminUser, "Corona").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new[] { "Admin" }).Wait();
                }
            }
        }
    }
}