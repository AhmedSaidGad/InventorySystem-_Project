using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}