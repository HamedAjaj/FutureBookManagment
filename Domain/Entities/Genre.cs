namespace FutureOFTask.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; } =  Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
