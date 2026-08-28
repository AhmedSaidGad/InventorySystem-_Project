using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "SKU is required.")]
        [MaxLength(50, ErrorMessage = "SKU cannot exceed 50 characters.")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }

       
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Low stock threshold is required.")]
        public int LowStockThreshold { get; set; }
    }
}