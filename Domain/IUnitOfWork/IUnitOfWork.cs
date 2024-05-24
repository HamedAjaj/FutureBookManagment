using FutureOFTask.Repository.GenericRepo;

namespace FutureOFTask.Domain.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();

    }
}
