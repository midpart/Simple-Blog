using Newtonsoft.Json;
using Simple_BlogApi.CommonLib;
using Simple_BlogApi.Model.Entities;
using Simple_BlogApi.Repositories;
using System.Security.Claims;

namespace Simple_BlogApi.Services
{
    public interface IBlogService : IBaseService<Blog>
    {
        Task<Blog> CreateAsync(Blog entity);
        Task<Blog> UpdateAsync(Blog entity);
        Task DeleteBlog(long id);
        Task<(int, List<Blog>)> SearchAsync(int start, int length, string searchText, bool withDeleted = false, bool? isActive = null, string hasIds = "");
    }

    public class BlogService : BaseService<Blog>, IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository repository, AppDbContext context) : base(repository)
        {
            _blogRepository = repository;
            _context = context;
        }

        public virtual async Task<Blog> CreateAsync(Blog entity)
        {
            try
            {
                using (var trans = await _context.Database.BeginTransactionAsync())
                {
                    var savingPoint = "BeforeSave";
                    try
                    {
                        trans.CreateSavepoint(savingPoint);

                        await _blogRepository.AddAsync(entity);
                        await _blogRepository.SaveChangesAsync();

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.RollbackToSavepoint(savingPoint);
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entity;
        }

        public virtual async Task<Blog> UpdateAsync(Blog entity)
        {
            try
            {
                if (entity == null)
                    throw new KeyNotFoundException("Unable to find any blog with this information.");
                var oldObject = await _blogRepository.FindAsync(entity.Id);

                using (var trans = await _context.Database.BeginTransactionAsync())
                {
                    var savingPoint = "BeforeSave";
                    try
                    {
                        trans.CreateSavepoint(savingPoint);

                        oldObject.Title = entity.Title;
                        oldObject.Description = entity.Description;
                        oldObject.Status = entity.Status;

                        _blogRepository.Edit(oldObject);
                        await _blogRepository.SaveChangesAsync();

                        trans.Commit();

                        entity = oldObject;
                    }
                    catch (Exception)
                    {
                        trans.RollbackToSavepoint(savingPoint);
                        throw;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                entity = null;
                throw;
            }
            catch (Exception)
            {
                entity = null;
                throw;
            }

            return entity;
        }

        public virtual async Task DeleteBlog(long id)
        {
            try
            {
                var oldObject = await _blogRepository.FindAsync(id);

                using (var trans = await _context.Database.BeginTransactionAsync())
                {
                    var savingPoint = "BeforeSave";
                    try
                    {
                        trans.CreateSavepoint(savingPoint);

                        _blogRepository.Remove(oldObject);
                        await _blogRepository.SaveChangesAsync();

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.RollbackToSavepoint(savingPoint);
                        throw;
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int, List<Blog>)> SearchAsync(int start, int length, string searchText, bool withDeleted = false, bool? isActive = null, string hasIds = "")
        {
            try
            {
                return  await _blogRepository.SearchAsync(start, length, searchText, withDeleted, isActive, hasIds);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
