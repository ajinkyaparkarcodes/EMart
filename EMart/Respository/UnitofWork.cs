using EMart.Data;
using EMart.Respository.IRepository;

namespace EMart.Respository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }

        public UnitofWork(ApplicationDbContext db) 
        {
            this._db = db;
            CategoryRepository = new CategoryRepository(_db);
            ProductRepository = new ProductRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
