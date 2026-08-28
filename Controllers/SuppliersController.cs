
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class SuppliersController : Controller
    {
      
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5; 

          
            var suppliers = _context.Suppliers.AsQueryable();

          
            if (!string.IsNullOrEmpty(searchTerm))
            {
                suppliers = suppliers.Where(s =>
                    s.SupplierName.Contains(searchTerm) ||  
                    (s.ContactName != null && s.ContactName.Contains(searchTerm)) || 
                    (s.Phone != null && s.Phone.Contains(searchTerm)) ||
                    (s.Email != null && s.Email.Contains(searchTerm)) || 
                    (s.Address != null && s.Address.Contains(searchTerm)) 
                );
            }

            
            int totalItems = suppliers.Count();

            
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

           
            var items = suppliers
                .OrderBy(s => s.SupplierID)        
                .Skip((page - 1) * pageSize)         
                .Take(pageSize)                      
                .ToList();

           
            var viewModels = new List<SupplierViewModel>();
            foreach (var s in items)
            {
                viewModels.Add(new SupplierViewModel
                {
                    SupplierID = s.SupplierID,
                    SupplierName = s.SupplierName,
                    ContactName = s.ContactName,
                    Phone = s.Phone,
                    Email = s.Email,
                    Address = s.Address
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
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null) return NotFound();

            var viewModel = new SupplierViewModel
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    SupplierName = viewModel.SupplierName,
                    ContactName = viewModel.ContactName,
                    Phone = viewModel.Phone,
                    Email = viewModel.Email,
                    Address = viewModel.Address
                };

                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

       
        public IActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null) return NotFound();

            var viewModel = new SupplierViewModel
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SupplierViewModel viewModel)
        {
            if (id != viewModel.SupplierID) return NotFound();

            if (ModelState.IsValid)
            {
                var supplier = _context.Suppliers.Find(id);
                if (supplier == null) return NotFound();

                supplier.SupplierName = viewModel.SupplierName;
                supplier.ContactName = viewModel.ContactName;
                supplier.Phone = viewModel.Phone;
                supplier.Email = viewModel.Email;
                supplier.Address = viewModel.Address;

                _context.Suppliers.Update(supplier);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

      
        public IActionResult Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null) return NotFound();

            var viewModel = new SupplierViewModel
            {
                SupplierID = supplier.SupplierID,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return View(viewModel);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}