using DMP.Models;
using System.Linq.Expressions;

namespace DMP.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Add(Product entity);

        Task<Product> Get(Expression<Func<Product, bool>> filter);

        Task<IEnumerable<Product>> GetAll(int pageNumber = 1, int pageSize = 10);

        Task<IEnumerable<Product>> GellAllWithCategoryAsync(int? categoryId, int pageNumber = 1, int pageSize = 10);

        Task<int> GetTotalItemCount();

        void Remove(Product entity);

        void RemoveRange(IEnumerable<Product> entities);

        Task Save();

        void Update(Product product);
    }
}
