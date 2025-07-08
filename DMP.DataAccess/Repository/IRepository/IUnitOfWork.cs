namespace DMP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork<T>
    {
        public void Save();
        public void Update(T entity);
    }
}
