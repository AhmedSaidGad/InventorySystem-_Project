using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Supplier
    {
        
        [Key]
        public int SupplierID { get; set; }

      
        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(150, ErrorMessage = "Supplier name cannot exceed 150 characters.")]
        public string SupplierName { get; set; }

        [MaxLength(100)]
        public string? ContactName { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

       
        public virtual List<Purchase>? Purchases { get; set; }
    }
}