using Simple_BlogApi.Model.Entities;
using Simple_BlogApi.Repositories;

namespace Simple_BlogApi.Services
{
    public interface IBaseService<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Edit(TEntity entity);
        void Remove(long id);
        void Remove(TEntity entityToDelete);
        IList<TEntity> LoadAll();
        Task<TEntity> FindAsync(long id);
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual void Edit(TEntity entity)
        {
            _repository.Edit(entity);
            _repository.SaveChangesAsync();
        }

        public virtual void Remove(long id)
        {
            _repository.Remove(id);
            _repository.SaveChangesAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _repository.SaveChangesAsync();
        }

        public virtual IList<TEntity> LoadAll()
        {
            return _repository.LoadAll();
        }

        public virtual async Task<TEntity> FindAsync(long id)
        {
            return await _repository.FindAsync(id);
        }
    }
}
