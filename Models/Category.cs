using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [MaxLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [MaxLength(500)] 
        public string? Description { get; set; }

        
        public virtual List<Product>? Products { get; set; }
    }
}