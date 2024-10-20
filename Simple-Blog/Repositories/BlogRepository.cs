using Microsoft.EntityFrameworkCore;
using Simple_Blog.CommonLib;
using Simple_Blog.Model.Entities;
using System.Linq;
using System.Threading;

namespace Simple_Blog.Repositories
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
        Task<(int, List<Blog>)> SearchAsync(int start, int length, string searchText, bool withDeleted, bool? isActive, string hasIds = "");
    }

    public class BlogRepository : BaseRepository<Blog, AppDbContext>, IBlogRepository
    {
        public BlogRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<(int, List<Blog>)> SearchAsync(int start, int length, string searchText, bool withDeleted, bool? isActive, string hasIds = "")
        {
            var queryable = Query();

            if (withDeleted == false)
                queryable = queryable.Where(x => x.Status != Blog.EntityStatus.Delete);
            if (isActive.HasValue)
                queryable = queryable.Where(x => x.Status == (isActive == true ? Blog.EntityStatus.Active : Blog.EntityStatus.Inactive));

            if (string.IsNullOrWhiteSpace(searchText) == false)
                queryable = queryable.Where(x => x.Title.Contains(searchText));

            if (string.IsNullOrWhiteSpace(hasIds) == false)
            {
                var ids = hasIds.Split(',').Where(x => string.IsNullOrWhiteSpace(x) == false).Select(x => long.TryParse(x, out long id) == true ? id : 0).Distinct().ToList();

                if (ids.Any())
                    queryable = queryable.Where(x => ids.Contains(x.Id) == false);
            }
            queryable = queryable.OrderByDescending(x => x.CreationDate);

            int rowCount = await queryable.CountAsync();
            if (length > 0)
                queryable = queryable.Skip(start).Take(length);

            return (rowCount, await queryable.ToListAsync());
        }
    }
}
