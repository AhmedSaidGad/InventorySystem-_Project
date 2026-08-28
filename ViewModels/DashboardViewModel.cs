namespace InventorySystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalStockQuantity { get; set; }
        public int LowStockProducts { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal TotalSales { get; set; }
    }
}