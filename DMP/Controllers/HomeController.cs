using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetMasteryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                return View(new List<Product>());
            }
            try
            {
                var categories = _context.Categories.ToList();
                //ViewBag.CurrentPage = pageNumber;
                //ViewBag.PageSize = pageSize;
                //var totalItems = await _productRepository.GetTotalItemCount();
                //ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                var products = await _productRepository.GetAll(pageNumber, pageSize);
                if (products == null || !products.Any())
                {
                    return View(new List<Product>());
                }                
                return View(products);
            }
            catch
            {
                return View(new List<Product>());
            }
        }

        public IActionResult Privacy(string name)
        {
            ViewBag.UserName = name ?? "Guest";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
