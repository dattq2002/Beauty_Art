using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using Services.Service;
using Services.Service.Interface;
using System;
using System.Linq.Expressions;

namespace Services.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        private readonly ICurrentTimeService _currentTime;
        private readonly IClaimsServices _claimsServices;

        public GenericRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices)
        {
            _dbSet = context.Set<T>();
            _currentTime = currentTime;
            _claimsServices = claimsServices;
        }
        public async Task CreateAsync(T entity)
        {
            entity.CreationDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.CreatedBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            await _dbSet.AddAsync(entity);
        }
        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _currentTime.GetCurrentTime();
                var id = _claimsServices.GetCurrentUser();
                entity.CreatedBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        public void UpdateAsync(T entity)
        {
            entity.ModificationDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.ModificationBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            _dbSet.Update(entity);
        }
        public void DeleteAsync(T entity)
        {
            entity.DeletionDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.DeleteBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().FirstOrDefaultAsync();

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<T> GetEntityByIdAsync(string id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }
    }
}