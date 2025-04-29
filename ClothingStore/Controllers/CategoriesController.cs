using ClothingStore.Data;
using ClothingStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Categories
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // GET: /Categories/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Categories/Add
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("All");
        }
    }
}
