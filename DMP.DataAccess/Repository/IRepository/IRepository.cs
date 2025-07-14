using System.Linq.Expressions;

namespace DMP.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int pageNumber = 1, int pageSize = 10);
        Task<T> Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> GetTotalItemCount();

    }
}
