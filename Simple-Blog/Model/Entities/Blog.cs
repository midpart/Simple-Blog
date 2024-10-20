using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Blog.Model.Entities
{
    public class Blog :BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
