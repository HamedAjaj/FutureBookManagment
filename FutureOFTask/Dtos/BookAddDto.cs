namespace FutureOFTask.Dtos
{
    public class BookAddDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public Guid GenreId { get; set; }
    }
}
