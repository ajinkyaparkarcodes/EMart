using System.Linq.Expressions;

namespace EMart.Respository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(string? IncludeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? IncludeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> enitity);
    }
}
