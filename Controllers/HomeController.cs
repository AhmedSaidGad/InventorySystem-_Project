
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalCategories = _context.Categories.Count();
            ViewBag.TotalSuppliers = _context.Suppliers.Count();
            ViewBag.TotalStock = _context.Products.Sum(p => p.StockQuantity);
            return View();
        }
    }
}