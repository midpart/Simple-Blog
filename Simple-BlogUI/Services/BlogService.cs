using Simple_BlogUI.Models.ViewModels;

namespace Simple_BlogUI.Services
{
    public interface IBlogService
    {
        Task<List<BlogViewModel>> GetBlogs();
        Task<BlogViewModel> GetBlog(long id);
        Task<bool> SaveBlog(BlogCreateUpdateViewModel blogVM);
        Task<bool> UpdateBlog(BlogCreateUpdateViewModel blogVM);
        Task<bool> DeleteBlog(long blogId);
        Task<(int, List<BlogViewModel>)> SearchBlogs(int start, int length, string searchText = "", bool withDeleted = false, bool? isActive = null, List<BlogViewModel>? hasBlogs = null);
    }

    public class BlogService : IBlogService
    {
        private readonly ILogger<BlogService> _logger;
        private readonly HttpClient httpClient;

        public BlogService(HttpClient httpClient, ILogger<BlogService> logger)
        {
            this.httpClient = httpClient;
            this._logger = logger;
        }

        public async Task<BlogViewModel?> GetBlog(long id)
        {
            BlogViewModel? item = null;
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

                var response = await httpClient.GetAsync($"api/Blogs/Get/{id}");

                if (response.IsSuccessStatusCode)
                    item = await response.Content.ReadFromJsonAsync<BlogViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return item;
        }

        public async Task<List<BlogViewModel>> GetBlogs()
        {
            var itemList = new List<BlogViewModel>();
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

                var response = await httpClient.GetAsync("api/Blogs/Load");

                if (response.IsSuccessStatusCode)
                    itemList = await response.Content.ReadFromJsonAsync<List<BlogViewModel>>();

                itemList = itemList ?? new List<BlogViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //throw;
            }

            return itemList;
        }

        public async Task<(int, List<BlogViewModel>)> SearchBlogs(int start, int length,  string searchText = "", bool withDeleted = false, bool? isActive = null, List<BlogViewModel>? hasBlogs = null)
        {
            var itemList = new List<BlogViewModel>();
            var totalItem = 0;
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

               var hasIds =  hasBlogs != null && hasBlogs.Any() ? string.Join(", ", hasBlogs.Select(x => x.BlogId).ToList()) : "";

                var response = await httpClient.GetAsync($"api/Blogs/SearchBlogs?start={start}&length={length}&searchText={searchText}&withDeleted={withDeleted}&isActive={isActive}&hasIds={hasIds}");

                SearchBlogViewModel? searchBlogVM = null;
                if (response.IsSuccessStatusCode)
                    searchBlogVM = await response.Content.ReadFromJsonAsync<SearchBlogViewModel>();

                if (searchBlogVM != null)
                {
                    itemList = searchBlogVM.BlogDtoList?? new List<BlogViewModel>();
                    totalItem = searchBlogVM.TotalBlog;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                itemList =  new List<BlogViewModel>();
                //throw;
            }

            return (totalItem, itemList);
        }

        public async Task<bool> SaveBlog(BlogCreateUpdateViewModel blogVM)
        {
            var isSuccess = false;
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

                var response = await httpClient.PostAsJsonAsync("api/Blogs/Create", blogVM);

                if (response.IsSuccessStatusCode)
                    isSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //throw;
            }

            return isSuccess;
        }

        public async Task<bool> UpdateBlog(BlogCreateUpdateViewModel blogVM)
        {
            var isSuccess = false;
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

                var response = await httpClient.PostAsJsonAsync("api/Blogs/Update", blogVM);

                if (response.IsSuccessStatusCode)
                    isSuccess = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //throw;
            }

            return isSuccess;
        }

        public async Task<bool> DeleteBlog(long blogId)
        {
            var isSuccess = false;
            try
            {
                if (httpClient == null)
                    throw new InvalidDataException("Invalid http client");

                var response = await httpClient.DeleteAsync($"api/Blogs/{blogId}");

                if (response.IsSuccessStatusCode)
                    isSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //throw;
            }

            return isSuccess;
        }
    }
}
