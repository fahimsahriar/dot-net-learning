using DMP.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMP.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Add(Category entity);

        Task<Category> Get(Expression<Func<Category, bool>> filter);

        Task<IEnumerable<Category>> GetAll(int pageNumber = 1, int pageSize = 10);

        Task<int> GetTotalItemCount();

        void Remove(Category entity);

        void RemoveRange(IEnumerable<Category> entities);

        Task Save();

        void Update(Category category);
    }
}
