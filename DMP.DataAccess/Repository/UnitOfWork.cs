using DMP.DataAccess.Data;
using DMP.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DMP.DataAccess.Repository
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
