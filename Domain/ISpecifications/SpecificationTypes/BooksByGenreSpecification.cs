using FutureOFTask.Domain.Entities;

namespace FutureOFTask.Domain.ISpecifications.SpecificationTypes
{
    public class BooksByGenreSpecification : BaseSpecification<Book>
    {
        public BooksByGenreSpecification(string genreName):base(b=>b.Genre.Name == genreName)
        {
            Includes.Add(g=>g.Genre);            
        }
    }
}
