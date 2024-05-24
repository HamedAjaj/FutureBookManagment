using FutureOFTask.Domain.ISpecifications;

namespace FutureOFTask.Repository.GenericRepo
{
    public interface IGenericRepository <T> where T : class
    {
        Task<T> GetByTitleAsync(string title);
        Task<T> GetByAuthorAsync(string author);
        Task<T> GetByWithSpecificationAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetByAllWithSpecificationAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void UpdateBook(T entity);
    }
}
