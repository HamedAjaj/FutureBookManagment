using Microsoft.VisualBasic;

namespace FutureOFTask.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public string Genre { get; set; }

    }
}
