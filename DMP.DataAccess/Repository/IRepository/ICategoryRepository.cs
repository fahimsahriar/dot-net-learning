using DMP.Models;

namespace DMP.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        // Additional methods specific to Category can be added here if needed
        void Save(); // This method can be used to save changes to the database
    }
}
