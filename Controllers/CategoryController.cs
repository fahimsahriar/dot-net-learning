using DotNetMasteryProject.Data;
using DotNetMasteryProject.Models;
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
                List<Category> categories = _db.Categories.ToList();
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
            Category? obj2ndWay = _db.Categories.FirstOrDefault( c => c.Name == "Action"); // works on any kind of column
            Category? obj3rdWay = _db.Categories.Where(c => c.DisplayOrder == 2).FirstOrDefault(); // works on any kind of column
            if(obj == null )
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit( Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
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
                // _db.Categories.Add(obj);
                // _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            catch(Exception ex)
            {
                //throw Exception
                return View();
            }
            
        }
    }
}
