using Simple_BlogApi.Model.Entities;

namespace Simple_BlogApi.Model.Dtos
{
    public class CreateBlogDto
    {
        public int Status { get; set; }
        public string Title { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CreationDate { get; set; }

        public Blog GetBlogModel()
        {
            return new Blog
            {
                Status = Status == BaseEntity.EntityStatus.Active || Status == BaseEntity.EntityStatus.Inactive ? Status : BaseEntity.EntityStatus.Active,
                Title = Title,
                Description = Description,
                CreationDate = CreationDate == null ? DateTime.Now : CreationDate.Value,
            };
        }
    }
}
