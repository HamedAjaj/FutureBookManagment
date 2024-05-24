using FutureOFTask.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FutureOFTask.Service.TokenService
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    }
}
