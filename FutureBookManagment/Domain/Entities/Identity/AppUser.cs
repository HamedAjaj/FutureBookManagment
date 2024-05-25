using Microsoft.AspNetCore.Identity;

namespace FutureOFTask.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
