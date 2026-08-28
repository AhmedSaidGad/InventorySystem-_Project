
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class PurchasesController : Controller
    {
       
        private readonly AppDbContext _context;

        public PurchasesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5;

           
            var purchases = _context.Purchases.AsQueryable();

           
            if (!string.IsNullOrEmpty(searchTerm))
            {
                purchases = purchases.Where(p =>
                    _context.Suppliers.Any(s => s.SupplierID == p.SupplierID && s.SupplierName.Contains(searchTerm)) ||
                    p.PurchaseDate.ToString().Contains(searchTerm)
                );
            }

           
            int totalItems = purchases.Count();

           
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

          
            var items = purchases
                .OrderBy(p => p.PurchaseID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModels = new List<PurchaseViewModel>();
            foreach (var p in items)
            {
                var supplier = _context.Suppliers.Find(p.SupplierID);
                viewModels.Add(new PurchaseViewModel
                {
                    PurchaseID = p.PurchaseID,
                    SupplierID = p.SupplierID,
                    SupplierName = supplier?.SupplierName,
                    PurchaseDate = p.PurchaseDate,
                    TotalAmount = p.TotalAmount
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
            var purchase = _context.Purchases.Find(id);
            if (purchase == null) return NotFound();

            var supplier = _context.Suppliers.Find(purchase.SupplierID);
            var viewModel = new PurchaseViewModel
            {
                PurchaseID = purchase.PurchaseID,
                SupplierID = purchase.SupplierID,
                SupplierName = supplier?.SupplierName,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount
            };

            return View(viewModel);
        }

       
        public IActionResult Create()
        {
            
            var suppliers = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "SupplierName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var purchase = new Purchase
                {
                    SupplierID = viewModel.SupplierID,
                    PurchaseDate = viewModel.PurchaseDate,
                    TotalAmount = viewModel.TotalAmount
                };

                _context.Purchases.Add(purchase);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            
            var suppliers = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "SupplierName");
            return View(viewModel);
        }

       
        public IActionResult Edit(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null) return NotFound();

            var viewModel = new PurchaseViewModel
            {
                PurchaseID = purchase.PurchaseID,
                SupplierID = purchase.SupplierID,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount
            };

            var suppliers = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "SupplierName", purchase.SupplierID);
            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PurchaseViewModel viewModel)
        {
            if (id != viewModel.PurchaseID) return NotFound();

            if (ModelState.IsValid)
            {
                var purchase = _context.Purchases.Find(id);
                if (purchase == null) return NotFound();

                purchase.SupplierID = viewModel.SupplierID;
                purchase.PurchaseDate = viewModel.PurchaseDate;
                purchase.TotalAmount = viewModel.TotalAmount;

                _context.Purchases.Update(purchase);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var suppliers = _context.Suppliers.ToList();
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "SupplierName", viewModel.SupplierID);
            return View(viewModel);
        }

        
        public IActionResult Delete(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null) return NotFound();

            var supplier = _context.Suppliers.Find(purchase.SupplierID);
            var viewModel = new PurchaseViewModel
            {
                PurchaseID = purchase.PurchaseID,
                SupplierID = purchase.SupplierID,
                SupplierName = supplier?.SupplierName,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount
            };

            return View(viewModel);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}