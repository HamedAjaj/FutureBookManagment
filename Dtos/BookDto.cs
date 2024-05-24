using Microsoft.VisualBasic;

namespace FutureOFTask.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public string Genre { get; set; }
    }
}
