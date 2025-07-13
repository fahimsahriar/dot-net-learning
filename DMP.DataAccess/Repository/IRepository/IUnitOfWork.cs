namespace DMP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork<T>
    {
        public Task Save();
        public void Update(T entity);
    }
}
