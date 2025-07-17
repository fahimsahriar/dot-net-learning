using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DMP.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Product entity)
        {
            _dbContext.Products.AddAsync(entity);
        }

        public Task<Product> Get(Expression<Func<Product, bool>> filter)
        {
            return _dbContext.Products.FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<Product>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return await Task.FromResult<IEnumerable<Product>>(new List<Product>());
            }
            return await _dbContext.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GellAllWithCategoryAsync(int? categoryId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return await Task.FromResult<IEnumerable<Product>>(new List<Product>());
            }
            var query = _dbContext.Products
                            .Include(p => p.Category)
                            .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            var products = await query
                            .OrderBy(p => p.Name)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            //return await _dbContext.Products
            //    .Include(p => p.Category)
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();
            return products;
        }

        public Task<int> GetTotalItemCount()
        {
            return _dbContext.Products.CountAsync();
        }

        public void Remove(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Product cannot be null");
            }
            _dbContext.Products.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            _dbContext.Products.RemoveRange(entities);
        }

        public async Task Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
