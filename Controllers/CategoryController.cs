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
            List<Category> categories = _db.Categories.ToList();
            Console.WriteLine(categories);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
