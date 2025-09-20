using System.Linq.Expressions;

namespace Concesionaria.API.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<TResult>> GetSelectedAsync<TResult>(Expression<Func<T, TResult>> selector);
    }
}