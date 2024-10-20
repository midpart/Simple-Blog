using Microsoft.AspNetCore.Components;
using Simple_BlogUI.Models.ViewModels;
using Simple_BlogUI.Services;

namespace Simple_BlogUI.Pages
{
    public partial class Index
    {
        private List<BlogViewModel> Blogs { get; set; }
        private bool HasMoreButton { get; set; }
        private int Length { get; set; } = BlogConstant.DisplayPerPage;
        private int Start { get; set; } = 0;
        [Inject]
        private IBlogService _blogService { get; set; }
        [Inject]
        private NavigationManager navManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var totalBlogs = 0;
            (totalBlogs, Blogs) = await _blogService.SearchBlogs(Start, Length, isActive: true);
            HasMoreButton = Blogs.Count() < totalBlogs;

            await base.OnInitializedAsync();
        }

        private async void DeleteBlog(long blogId)
        {
            var isSuccess = await _blogService.DeleteBlog(blogId);
            if (isSuccess)
                Blogs = Blogs.Where(x => x.BlogId != blogId).ToList();
        }

        private async void ViewMoreBlogEvent()
        {
            (var totalBlogs, var extraBlogs) = await _blogService.SearchBlogs(Start, Length, isActive: true, hasBlogs: Blogs);
            HasMoreButton = Blogs.Count() < totalBlogs;

            if (extraBlogs != null && extraBlogs.Any())
            {
                Blogs.AddRange(extraBlogs);
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
        }

        private void GoToNewBlog()
        {
            navManager.NavigateTo($"./{BlogConstant.NewBlog}");
        }
    }
}
