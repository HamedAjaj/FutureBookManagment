using FutureOFTask.Domain.Entities;
namespace FutureOFTask.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset PublicationDate { get; set; }

        public Guid GenreId { get; set; }
        public string? GenreName { get; set; }
        public double rate { get; set; }
    }
}
