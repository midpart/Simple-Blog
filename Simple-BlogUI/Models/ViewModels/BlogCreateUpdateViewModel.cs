using System.ComponentModel.DataAnnotations;

namespace Simple_BlogUI.Models.ViewModels
{
    public class BlogCreateUpdateViewModel
    {
        public long BlogId { get; set; }
        public int Status { get; set; }

        [Required]
        [MinLength(5, ErrorMessage ="Blog title is too short")]
        [MaxLength(255, ErrorMessage = "Blog title is too long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage = "Blog description is too short")]
        public string Description { get; set; } = string.Empty;
    }
}
