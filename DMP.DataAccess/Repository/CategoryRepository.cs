using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;

namespace DMP.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UnitOfWork<Category> _unitOfWork;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _unitOfWork = new UnitOfWork<Category>(_dbContext);
        }
        public async Task Save()
        {
            //_dbContext.SaveChanges();
            await _unitOfWork.Save();
        }

        public void Update(Category category)
        {
            //_dbContext.Categories.Update(category);
            _unitOfWork.Update(category);
        }
    }
}
