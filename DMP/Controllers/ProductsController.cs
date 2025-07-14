using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace DotNetMasteryProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductsController(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
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
                var products = await _productRepository.GetAll(pageNumber, pageSize);
                if (products == null || !products.Any())
                {
                    return View(new List<Product>());
                }
                ViewBag.CurrentPage = pageNumber;
                ViewBag.PageSize = pageSize;
                var totalItems = await _productRepository.GetTotalItemCount();
                ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
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
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,CreatedAt,UpdatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                await _productRepository.Save();
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Stock,CreatedAt,UpdatedAt")] Product product)
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
