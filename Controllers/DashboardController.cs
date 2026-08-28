
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
            var viewModel = new DashboardViewModel
            {
                TotalProducts = _context.Products.Count(),
                TotalCategories = _context.Categories.Count(),
                TotalSuppliers = _context.Suppliers.Count(),
                TotalStockQuantity = _context.Products.Sum(p => p.StockQuantity),
                LowStockProducts = _context.Products.Count(p => p.StockQuantity <= p.LowStockThreshold),
                TotalPurchases = _context.Purchases.Sum(p => p.TotalAmount),
                TotalSales = _context.Sales.Sum(s => s.TotalAmount)
            };

            
            var recentSales = _context.Sales
                .OrderByDescending(s => s.SaleDate)
                .Take(10)
                .Select(s => new SaleViewModel
                {
                    SaleID = s.SaleID,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount,
                    CustomerInfo = s.CustomerInfo
                })
                .ToList();

            ViewBag.RecentSales = recentSales;

            var recentPurchases = _context.Purchases
                .OrderByDescending(p => p.PurchaseDate)
                .Take(5)
                .Select(p => new PurchaseViewModel
                {
                    PurchaseID = p.PurchaseID,
                    PurchaseDate = p.PurchaseDate,
                    TotalAmount = p.TotalAmount,
                    SupplierName = _context.Suppliers.FirstOrDefault(s => s.SupplierID == p.SupplierID).SupplierName
                })
                .ToList();

            ViewBag.RecentPurchases = recentPurchases;

            var lowStockProducts = _context.Products
                .Where(p => p.StockQuantity <= p.LowStockThreshold)
                .OrderBy(p => p.StockQuantity)
                .Take(5)
                .Select(p => new ProductViewModel
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    SKU = p.SKU,
                    StockQuantity = p.StockQuantity,
                    LowStockThreshold = p.LowStockThreshold
                })
                .ToList();

            ViewBag.LowStockProducts = lowStockProducts;

            return View(viewModel);
        }
    }
}