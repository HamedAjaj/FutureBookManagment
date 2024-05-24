using FutureOFTask.Domain;
using FutureOFTask.Domain.ISpecifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FutureOFTask.Repository.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery; //_dbContext.book
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria); // _dbContext.book.where(p => p.id==id) or another

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            // _dbContext.book.where(p => p.id == id).Include(p=>p.fk).Include(p=>p.fk)    ------
            return query;

        }

    }
}
