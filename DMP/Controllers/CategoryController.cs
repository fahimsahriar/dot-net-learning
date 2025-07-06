using DMP.DataAccess.Data;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMasteryProject.Controllers
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
            try
            {
                var categories = _db.Categories
                .OrderBy(c => c.DisplayOrder)
                .ToList();
                return View(categories);
            }
            catch
            {
                return View(new List<Category>());
            }

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //adding custom validation 
                ModelState.AddModelError("name", "Display order and name can not be same.");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                //throw Exception
                return View();
            }

        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id < 1 || id == null)
            {
                return NotFound();
            }
            Category? obj = _db.Categories.Find(id); //works on primary key
            Category? obj2ndWay = _db.Categories.FirstOrDefault(c => c.Name == "Action"); // works on any kind of column
            Category? obj3rdWay = _db.Categories.Where(c => c.DisplayOrder == 2).FirstOrDefault(); // works on any kind of column
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order and name cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            try
            {
                var objFromDb = _db.Categories.FirstOrDefault(c => c.Category21Id == obj.Category21Id);
                if (objFromDb == null)
                {
                    return NotFound();
                }

                // Manually update properties
                objFromDb.Name = obj.Name;
                objFromDb.DisplayOrder = obj.DisplayOrder;

                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index", "Category");
            }
            catch (Exception)
            {
                return View(obj);
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id < 1 || id == null)
            {
                return NotFound();
            }
            Category? obj2ndWay = _db.Categories.FirstOrDefault(c => c.Category21Id == id);
            if (obj2ndWay == null)
            {
                return NotFound();
            }
            try
            {
                _db.Categories.Remove(obj2ndWay);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index", "Category");
            }
            catch
            {
                return RedirectToAction("Index", "Category");
            }
        }
    }
}
