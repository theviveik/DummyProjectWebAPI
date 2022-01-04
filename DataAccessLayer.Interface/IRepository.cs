using System.Linq.Expressions;

namespace DataAccessLayer.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);

        T GetById(long id);

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}