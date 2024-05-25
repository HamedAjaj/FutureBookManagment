using FutureOFTask.Domain.ISpecifications;

namespace FutureOFTask.Repository.GenericRepo
{
    public interface IGenericRepository <T> where T : class
    {
       
        Task<T> GetByWithSpecificationAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void UpdateBook(T entity);
        void DeleteBook(T entity);

    }
}
