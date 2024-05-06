namespace EMart.Respository.IRepository
{
    public interface IUnitofWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        void Save();
    }
}
