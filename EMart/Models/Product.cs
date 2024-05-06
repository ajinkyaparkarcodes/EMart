using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMart.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Cannot be Empty")]
        [MaxLength(50)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Cannot be Empty")]
        [MaxLength(80)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Cannot be Empty")]
        [Range(10, 3000, ErrorMessage = "Price must be between 10-1000")]
        [DisplayName("Price")]
        public double ListPrice { get; set; }

        [Required(ErrorMessage = "Pack Size Value Cannot be Empty")]
        [DisplayName("Pack Size Value")]
        [Range(1.0, 10000.0, ErrorMessage = "Pack Size must be between 1.0-10000.0")]
        public double PackSizeValue { get; set; }

        [Required(ErrorMessage = "Pack Size Unit Cannot be Empty")]
        [DisplayName("Pack Size Unit")]
        public string PackSizeUnit { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [DisplayName("Product Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
