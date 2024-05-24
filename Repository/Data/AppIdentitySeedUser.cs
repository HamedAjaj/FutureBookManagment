using FutureOFTask.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FutureOFTask.Repository.Data
{
    public class AppIdentitySeedUser
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    Name = "Hamed Ajaj",
                    Email = "hamedajaj906@gmail.com",
                    UserName = "hamedajaj906",
                    PhoneNumber = "01033839067"
                };
                await userManager.CreateAsync(user, "Hamed@1234");
            }
        }

    }
}
