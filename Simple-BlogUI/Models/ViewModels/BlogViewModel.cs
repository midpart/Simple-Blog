namespace Simple_BlogUI.Models.ViewModels
{
    public class BlogViewModel
    {
        public long BlogId { get; set; }
        public int Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class SearchBlogViewModel
    {
        public int TotalBlog { get; set; }
        public List<BlogViewModel> BlogDtoList { get; set; }
    }
}
