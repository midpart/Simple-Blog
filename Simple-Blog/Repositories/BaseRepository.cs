using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Simple_Blog.Model.Entities;

namespace Simple_Blog.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task AddAsync(TEntity entity);
        TEntity Edit(TEntity entity);
        void Remove(long id);
        void Remove(TEntity entityToDelete);
        IList<TEntity> LoadAll();
        Task<TEntity> FindAsync(long id);
        Task<int> SaveChangesAsync();
        IQueryable<TEntity> Query();
    }

    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity
        where TContext : DbContext
    {
        protected TContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        private readonly IHttpContextAccessor _httpContext;

        public BaseRepository(TContext context, IHttpContextAccessor httpContext)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
            _httpContext = httpContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.ModificationDate = DateTime.Now;
            entity.VersionNumber = 1;
            if (entity.Status == 0)
            {
                entity.Status = BaseEntity.EntityStatus.Active;
            }

            await _dbSet.AddAsync(entity);
        }

        public virtual TEntity Edit(TEntity entity)
        {
            entity.ModificationDate = DateTime.Now;
            entity.VersionNumber = entity.VersionNumber + 1;
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;

        }

        public virtual void Remove(long id)
        {
            var entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
                Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            entityToDelete.ModificationDate = DateTime.Now;
            entityToDelete.VersionNumber = entityToDelete.VersionNumber + 1;
            entityToDelete.Status = BaseEntity.EntityStatus.Delete;
            _dbContext.Entry(entityToDelete).State = EntityState.Modified;
        }

        public IQueryable<TEntity> Query()
        {
            IQueryable<TEntity> query = _dbSet;

            return query;
        }

        public virtual IList<TEntity> LoadAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public virtual async Task<TEntity> FindAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
    }
}
