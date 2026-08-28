
using InventorySystem.Models;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(string searchTerm, int page = 1)
        {
            int pageSize = 5; 

            
            var categories = _context.Categories.AsQueryable(); //Deferred Execution


            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(c =>
                    c.CategoryName.Contains(searchTerm) ||
                    (c.Description != null && c.Description.Contains(searchTerm))
                );
            }

           
            int totalItems = categories.Count();

            
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            
            var items = categories
                .OrderBy(c => c.CategoryID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

           
            var viewModels = new List<CategoryViewModel>();
            foreach (var c in items)
            {
                viewModels.Add(new CategoryViewModel
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    Description = c.Description
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
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryViewModel
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryName = viewModel.CategoryName,
                    Description = viewModel.Description
                };

                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryViewModel
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryViewModel viewModel)
        {
            if (id != viewModel.CategoryID) return NotFound();

            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(id);
                if (category == null) return NotFound();

                category.CategoryName = viewModel.CategoryName;
                category.Description = viewModel.Description;

                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryViewModel
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}