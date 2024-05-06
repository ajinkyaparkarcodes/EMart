using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name Cannot be Empty")]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Display Order Cannot be Empty")]
        [Range(1,100,ErrorMessage = "Display Order must be between 1-100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set;}
    }
}
