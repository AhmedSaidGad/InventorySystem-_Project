using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class Product
    {
       
        [Key]
        public int ProductID { get; set; }

       
        [Required(ErrorMessage = "SKU is required.")]
        [MaxLength(50, ErrorMessage = "SKU cannot exceed 50 characters.")]
        public string SKU { get; set; }

       
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        
        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; set; }

       
        [Required(ErrorMessage = "Stock quantity is required.")]
        public int StockQuantity { get; set; } = 0; 


        [Required(ErrorMessage = "Low stock threshold is required.")]
        public int LowStockThreshold { get; set; } = 5;

        public virtual Category? Category { get; set; }

        
        public virtual List<PurchaseItem>? PurchaseItems { get; set; }
        public virtual List<SaleItem>? SaleItems { get; set; }
    }
}