using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DotNetMasteryProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(ApplicationDbContext context, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Products
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
                //var products = await _productRepository.GetAll(pageNumber, pageSize);
                var products = await _productRepository.GellAllWithCategoryAsync(pageNumber, pageSize);
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

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.Get(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                _productRepository.Add(product);
                await _productRepository.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            TempData["error"] = "Product creation failed.";
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.Get(m => m.Id == id);
            // Optionally load categories for dropdown
            ViewBag.Categories = await _categoryRepository.GetAll();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Price,Stock,CreatedAt,UpdatedAt")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                product.UpdatedAt = DateTime.Now; // Update the UpdatedAt field to current time
                try
                {
                    _productRepository.Update(product);
                    await _productRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.Get(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _productRepository.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            if(await _productRepository.Get(e => e.Id == id) != null)
                return true;
            else return false;
        }
    }
}
