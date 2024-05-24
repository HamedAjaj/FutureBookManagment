using FutureOFTask.Domain.Entities;

namespace FutureOFTask.Domain.ISpecifications.SpecificationTypes
{
    public class PopularBooksSpecification : BaseSpecification<Book>
    {
        public PopularBooksSpecification(int topN) : base(null)
        {
            Includes.Add(b => b.Ratings);

            AddOrderByDesc(b => b.Ratings.Average(r => r.Score));
            AddOrderByDesc(b => b.Ratings.Count);

            ApplyPagination(0, topN); // Get the top N books
        }
    }
}
