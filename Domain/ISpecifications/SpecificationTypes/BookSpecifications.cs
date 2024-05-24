using FutureOFTask.Domain.Entities;

namespace FutureOFTask.Domain.ISpecifications.SpecificationTypes
{
    public class BookSpecifications : BaseSpecification<Book>
    {
        // searching by 
        public BookSpecifications(BookSpecParams specParams) : base(b=>
            (string.IsNullOrEmpty(specParams.Search) || b.Title.ToLower().Contains(specParams.Search) || b.Author.ToLower().Contains(specParams.Search) )  &&
            (string.IsNullOrEmpty(specParams.title) || b.Title == specParams.title) &&
            (string.IsNullOrEmpty(specParams.author) || b.Title == specParams.author) &&
            (string.IsNullOrEmpty(specParams.GenreName) || b.Genre.Name == specParams.GenreName) 
        )
        {
            Includes.Add(b=>b.Genre);
            Includes.Add(b=>b.Ratings); 

            // Sorting Filter 
            AddOrderBy(b=>b.PublicationDate);

            
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "Title":
                        AddOrderBy(b=>b.Title); 
                                                  
                        break;
                    case "Author":
                        AddOrderBy(b => b.Author);
                        break;
                    case "GenreName":
                        AddOrderBy(b => b.Genre.Name);
                        break;
                    default:
                        AddOrderBy(b=>b.PublicationDate);
                        break;
                }
            }
        }

        public BookSpecifications(string id):base(b=>b.Id.ToString() == id)
        {
            Includes.Add(b => b.Ratings);
        }

        public BookSpecifications()
        { }
    }
}
