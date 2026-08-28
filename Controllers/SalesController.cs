
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5; 

            
            var sales = _context.Sales.AsQueryable();

            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                sales = sales.Where(s =>
                    (s.CustomerInfo != null && s.CustomerInfo.Contains(searchTerm)) ||
                    s.SaleDate.ToString().Contains(searchTerm)
                );
            }

            
            int totalItems = sales.Count();

            
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = sales
                .OrderBy(s => s.SaleID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

         
            var viewModels = new List<SaleViewModel>();
            foreach (var s in items)
            {
                viewModels.Add(new SaleViewModel
                {
                    SaleID = s.SaleID,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount,
                    CustomerInfo = s.CustomerInfo
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
            var sale = _context.Sales.Find(id);
            if (sale == null) return NotFound();

            var viewModel = new SaleViewModel
            {
                SaleID = sale.SaleID,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                CustomerInfo = sale.CustomerInfo
            };

            return View(viewModel);
        }

       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sale = new Sale
                {
                    SaleDate = viewModel.SaleDate,
                    TotalAmount = viewModel.TotalAmount,
                    CustomerInfo = viewModel.CustomerInfo
                };

                _context.Sales.Add(sale);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

       
        public IActionResult Edit(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale == null) return NotFound();

            var viewModel = new SaleViewModel
            {
                SaleID = sale.SaleID,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                CustomerInfo = sale.CustomerInfo
            };

            return View(viewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SaleViewModel viewModel)
        {
            if (id != viewModel.SaleID) return NotFound();

            if (ModelState.IsValid)
            {
                var sale = _context.Sales.Find(id);
                if (sale == null) return NotFound();

                sale.SaleDate = viewModel.SaleDate;
                sale.TotalAmount = viewModel.TotalAmount;
                sale.CustomerInfo = viewModel.CustomerInfo;

                _context.Sales.Update(sale);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale == null) return NotFound();

            var viewModel = new SaleViewModel
            {
                SaleID = sale.SaleID,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                CustomerInfo = sale.CustomerInfo
            };

            return View(viewModel);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}