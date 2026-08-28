using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class SaleItem
    {
        
        [Key]
        public int SaleItemID { get; set; }

      
        [ForeignKey("Sale")]
        public int SaleID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

       
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

      
        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; set; }

        public virtual Sale? Sale { get; set; }

        public virtual Product? Product { get; set; }
    }
}