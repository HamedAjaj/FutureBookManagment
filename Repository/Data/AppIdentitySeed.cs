using FutureOFTask.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FutureOFTask.Repository.Data
{
    public class AppIdentitySeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // check if this role if Exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    Name = "Hamed Ajaj",
                    Email = "hamedajaj906@gmail.com",
                    UserName = "hamedajaj906",
                    PhoneNumber = "01033839067"
                };
                var rsult = await userManager.CreateAsync(user, "Hamed@1234");
                if (rsult.Succeeded) { await userManager.AddToRoleAsync(user, "Admin"); }
            }
        }


        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

    }
}
