using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public class PurchaseViewModel
    {
        public int PurchaseID { get; set; }

        [Required(ErrorMessage = "Supplier is required.")]
        public int SupplierID { get; set; }

       
        public string? SupplierName { get; set; }

        [Required(ErrorMessage = "Purchase date is required.")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Total amount is required.")]
        public decimal TotalAmount { get; set; }
    }
}