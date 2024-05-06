using EMart.Data;
using EMart.Models;
using EMart.Respository.IRepository;

namespace EMart.Respository
{
    public class CategoryRepository :  Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db=db;
        }
       
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
