using FutureOFTask.Domain.Entities;

namespace FutureOFTask.Domain.ISpecifications.SpecificationTypes
{
    public class BookSpecifications : BaseSpecification<Book>
    {
        // searching in queryparams
        public BookSpecifications(BookSpecParams specParams) : base(b=>
            (string.IsNullOrEmpty(specParams.Search) || b.Title.ToLower().Contains(specParams.Search) || b.Author.ToLower().Contains(specParams.Search) )  &&
            (string.IsNullOrEmpty(specParams.title) || b.Title == specParams.title) &&
            (string.IsNullOrEmpty(specParams.author) || b.Title == specParams.author)
        )
        {

            // Sorting
            AddOrderBy(b=>b.PublicationDate);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "Title":
                        AddOrderBy(b=>b.Title); //this is more readable if it act as function
                                                  
                        break;
                    case "Author":
                        AddOrderBy(b => b.Author);
                        break;
                    default:
                        AddOrderBy(b=>b.PublicationDate);
                        break;
                }
            }


        }

        public BookSpecifications(string id):base(b=>b.Id.ToString() == id)
        {
            
        }
    }
}
