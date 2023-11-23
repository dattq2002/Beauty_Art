using Microsoft.EntityFrameworkCore.Query;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using System.Linq.Expressions;

namespace Services.Repository
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<T> GetEntityByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<ICollection<T>> GetListAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
