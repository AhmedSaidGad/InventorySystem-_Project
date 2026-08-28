using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class Purchase
    {
      
        [Key]
        public int PurchaseID { get; set; }

        
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        
        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

         
        [Required(ErrorMessage = "Total amount is required.")]
        public decimal TotalAmount { get; set; }

       
        public virtual Supplier? Supplier { get; set; }

        
         public virtual List<PurchaseItem>? PurchaseItems { get; set; }
    }
}