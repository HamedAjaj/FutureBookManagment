using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Repository.Data;
using FutureOFTask.Repository.GenericRepo;
using System.Collections;

namespace FutureOFTask.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>; // if cast not found then, hashtable will return object 
        }

        public async Task<int> Complete() => await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
