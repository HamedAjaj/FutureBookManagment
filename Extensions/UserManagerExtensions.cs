using FutureOFTask.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FutureOFTask.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<bool> GetByEmail(this UserManager<AppUser> userManager, string email)
              => await userManager.FindByEmailAsync(email) !=null;
           
    }
}
