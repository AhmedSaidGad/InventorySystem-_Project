
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class ProductsController : Controller
    {
      
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5; 

         
            var products = _context.Products.AsQueryable();

           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                    p.SKU.Contains(searchTerm) ||
                    p.ProductName.Contains(searchTerm)
                );
            }

           
            int totalItems = products.Count();

          
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

           
            var items = products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            
            var viewModels = new List<ProductViewModel>();
            foreach (var p in items)
            {
                var category = _context.Categories.Find(p.CategoryID);
                viewModels.Add(new ProductViewModel
                {
                    ProductID = p.ProductID,
                    SKU = p.SKU,
                    ProductName = p.ProductName,
                    CategoryID = p.CategoryID,
                    CategoryName = category?.CategoryName,
                    UnitPrice = p.UnitPrice,
                    StockQuantity = p.StockQuantity,
                    LowStockThreshold = p.LowStockThreshold
                });
            }

            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentSearch = searchTerm;
            ViewBag.ActionName = "Index";

            return View(viewModels);
        }

        
        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var category = _context.Categories.Find(product.CategoryID);
            var viewModel = new ProductViewModel
            {
                ProductID = product.ProductID,
                SKU = product.SKU,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                CategoryName = category?.CategoryName,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                LowStockThreshold = product.LowStockThreshold
            };

            return View(viewModel);
        }

       
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    SKU = viewModel.SKU,
                    ProductName = viewModel.ProductName,
                    CategoryID = viewModel.CategoryID,
                    UnitPrice = viewModel.UnitPrice,
                    StockQuantity = viewModel.StockQuantity,
                    LowStockThreshold = viewModel.LowStockThreshold
                };

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

           
            var categories = _context.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            return View(viewModel);
        }

       
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var viewModel = new ProductViewModel
            {
                ProductID = product.ProductID,
                SKU = product.SKU,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                LowStockThreshold = product.LowStockThreshold
            };

            var categories = _context.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductViewModel viewModel)
        {
            if (id != viewModel.ProductID) return NotFound();

            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(id);
                if (product == null) return NotFound();

                product.SKU = viewModel.SKU;
                product.ProductName = viewModel.ProductName;
                product.CategoryID = viewModel.CategoryID;
                product.UnitPrice = viewModel.UnitPrice;
                product.StockQuantity = viewModel.StockQuantity;
                product.LowStockThreshold = viewModel.LowStockThreshold;

                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = _context.Categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName", viewModel.CategoryID);
            return View(viewModel);
        }

        
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var category = _context.Categories.Find(product.CategoryID);
            var viewModel = new ProductViewModel
            {
                ProductID = product.ProductID,
                SKU = product.SKU,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                CategoryName = category?.CategoryName,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                LowStockThreshold = product.LowStockThreshold
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}