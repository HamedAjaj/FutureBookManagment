using FutureOFTask.Domain.Entities.Identity;

namespace FutureOFTask.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int Score { get; set; }
    }
}
