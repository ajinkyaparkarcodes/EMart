using EMart.Data;
using EMart.Models;
using EMart.Respository.IRepository;

namespace EMart.Respository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

       
        public void Update(Product obj)
        {
            var data = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if(data != null)
            {
                data.Name= obj.Name;
                data.Description= obj.Description;
                data.ListPrice=obj.ListPrice;
                data.PackSizeValue=obj.PackSizeValue;
                data.PackSizeUnit=obj.PackSizeUnit;
                data.CategoryId=obj.CategoryId;
                if(obj.ImageUrl!=null)
                {
                    data.ImageUrl=obj.ImageUrl;
                }
            }
        }
    }
}
