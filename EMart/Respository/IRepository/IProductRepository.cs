using EMart.Models;

namespace EMart.Respository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
      
    }
}
