using Simple_Blog.Model.Entities;

namespace Simple_Blog.Model.Dtos
{
    public class BlogDto
    {
        public long BlogId { get; set; }
        public int Status { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public static BlogDto GetBlogDto(Blog entity)
        {
            return new BlogDto
            {
                BlogId = entity.Id,
                Status = entity.Status,
                Title = entity.Title,
                Description = entity.Description,
                CreationDate = entity.CreationDate,
                ModificationDate = entity.ModificationDate,
            };
        }
    }

    public class SearchBlogDto
    {
        public int TotalBlog { get; set; }
        public List<BlogDto> BlogDtoList { get; set; }
    }
}
