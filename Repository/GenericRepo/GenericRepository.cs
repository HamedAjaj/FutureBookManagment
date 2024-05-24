
using FutureOFTask.Domain.ISpecifications;
using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Repository.Data;
using FutureOFTask.Repository.Specifications;
using Microsoft.EntityFrameworkCore;

namespace FutureOFTask.Repository.GenericRepo
{
    public class GenericRepository<T>  : IGenericRepository<T>  where T : class
    {
        private readonly BookDbContext _context;
        public GenericRepository(BookDbContext context) => _context = context;
        

        public async Task AddAsync(T entity)=> 
            await _context.Set<T>().AddAsync(entity);
       
        public void UpdateBook(T entity)=>  _context.Set<T>().Update(entity);

        public async Task<T> GetByWithSpecificationAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();
        

        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();

        public void DeleteBook(T entity)
            => _context.Set<T>().Remove(entity);

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).AsNoTracking();



    }
}
