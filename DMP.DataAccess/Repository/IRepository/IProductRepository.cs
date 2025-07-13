using DMP.Models;

namespace DMP.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product product);
        // Additional methods specific to Category can be added here if needed
        Task Save(); // This method can be used to save changes to the database
    }
}
