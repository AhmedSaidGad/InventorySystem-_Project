using System.ComponentModel.DataAnnotations;

namespace InventorySystem.ViewModels
{
    public class SaleViewModel
    {
        public int SaleID { get; set; }

        [Required(ErrorMessage = "Sale date is required.")]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Total amount is required.")]
        public decimal TotalAmount { get; set; }

        [MaxLength(500, ErrorMessage = "Customer info cannot exceed 500 characters.")]
        public string? CustomerInfo { get; set; }
    }
}