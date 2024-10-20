using Simple_Blog.Model.Entities;

namespace Simple_Blog.Model.Dtos
{
    public class UpdateBlogDto
    {
        public long BlogId { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Blog GetBlogModel()
        {
            return new Blog
            {
                Id = BlogId,
                Status = Status,
                Title = Title,
                Description = Description,
            };
        }
    }
}
