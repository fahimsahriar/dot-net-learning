using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace DMP.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }
        public async void Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
