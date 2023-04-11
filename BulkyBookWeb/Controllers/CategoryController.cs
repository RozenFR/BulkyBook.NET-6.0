using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _db.Categories;
            return View(objCategory);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent Forgery Attack
        public IActionResult Create(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(cat);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category categoryFromDb = _db.Categories.Find(id);
            // Category categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id); // Single => Throw an exception if more than 1 element
            // Category categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); // First => No exception
            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevent Forgery Attack
        public IActionResult Edit(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(cat);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category categoryFromDb = _db.Categories.Find(id);
            // Category categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id); // Single => Throw an exception if more than 1 element
            // Category categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); // First => No exception
            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Prevent Forgery Attack
        public IActionResult DeletePOST(int? id)
        {
            Category cat = _db.Categories.Find(id);
            if (cat == null) return NotFound();

            _db.Categories.Remove(cat);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
