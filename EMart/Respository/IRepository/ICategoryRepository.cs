using EMart.Models;

namespace EMart.Respository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
       
    }
}
