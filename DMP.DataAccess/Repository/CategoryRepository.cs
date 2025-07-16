using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;
using System.Linq.Expressions;

namespace DMP.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Category entity)
        {
            _dbContext.Categories.Add(entity);
        }

        public Task<Category> Get(Expression<Func<Category, bool>> filter)
        {
            return Task.FromResult(_dbContext.Categories.FirstOrDefault(filter));
        }

        public Task<IEnumerable<Category>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var categories = _dbContext.Categories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable();
            return Task.FromResult(categories);
        }

        public Task<int> GetTotalItemCount()
        {
            return Task.FromResult(_dbContext.Categories.Count());
        }

        public void Remove(Category entity)
        {
            _dbContext.Categories.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            _dbContext.Categories.RemoveRange(entities);
        }

        public async Task Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
