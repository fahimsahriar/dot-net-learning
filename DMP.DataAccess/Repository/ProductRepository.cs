using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using DMP.Models;

namespace DMP.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UnitOfWork<Product> _unitOfWork;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _unitOfWork = new UnitOfWork<Product>(dbContext);
        }
        public void Save()
        {
            //_dbContext.SaveChanges();
            _unitOfWork.Save();
        }

        public void Update(Product product)
        {
            //_dbContext.Products.Update(product);
            _unitOfWork.Update(product);
        }
    }
}
