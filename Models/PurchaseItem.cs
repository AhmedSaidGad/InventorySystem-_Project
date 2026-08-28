using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class PurchaseItem
    {
        
        [Key]
        public int PurchaseItemID { get; set; }

        [ForeignKey("Purchase")]
        public int PurchaseID { get; set; }

        
        [ForeignKey("Product")]
        public int ProductID { get; set; }

      
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        
        [Required(ErrorMessage = "Unit cost is required.")]
        public decimal UnitCost { get; set; }

        public virtual Purchase? Purchase { get; set; }

        
        public virtual Product? Product { get; set; }
    }
}