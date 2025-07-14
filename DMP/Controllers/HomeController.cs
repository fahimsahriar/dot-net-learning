using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;

namespace DotNetMasteryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                return View(new List<Product>());
            }
            try
            {
                ViewBag.CurrentPage = pageNumber;
                ViewBag.PageSize = pageSize;
                var totalItems = await _productRepository.GetTotalItemCount();
                ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
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
