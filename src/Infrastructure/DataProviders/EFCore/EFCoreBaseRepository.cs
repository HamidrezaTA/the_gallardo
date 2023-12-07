using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Base;

namespace Infrastructure.DataProviders.EFCore
{
    public class EFCoreBaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;

        public EFCoreBaseRepository(EFCoreDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllAsQuery(List<Expression<Func<TEntity, object>>>? includeExpressions = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(List<Expression<Func<TEntity, object>>>? includeExpressions = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAsNoTracking(List<Expression<Func<TEntity, object>>>? includeExpressions = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query.AsNoTracking();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TPrimaryKey id, List<Expression<Func<TEntity, object>>>? includeExpressions = null)
        {

            IQueryable<TEntity> query = _dbSet.Where(e => e.Id != null && e.Id.Equals(id));

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task HardDeleteAsync(TPrimaryKey id)
        {
            var link = await _dbSet.FindAsync(id);
            if (link is not null)
                _dbSet.Remove(link);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TPrimaryKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is not null)
            {
                entity.UpdatedAt = DateTimeOffset.Now;
                if (entity is BaseEntitySoftDelete<TPrimaryKey> entityAsSoftDelete)
                    entityAsSoftDelete.DeletedAt = DateTimeOffset.Now;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TPrimaryKey id, TEntity entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
                throw new InvalidOperationException("Entity not found"); // Handle not found

            // Update the properties of the existing entity with the values from the incoming entity
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();

            return existingEntity;
        }

        public async Task<List<TEntity>> GetAllPaginatedAsync(IQueryable<TEntity> query,
                                                              int? pageSize = null,
                                                              int? pageIndex = null)
        {
            int _pageSize = 1;
            int _pageIndex = 25;

            if (pageIndex.HasValue)
                _pageIndex = pageIndex.Value;
            if (pageSize.HasValue)
                _pageSize = pageSize.Value;

            query = query.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize);

            return await query.ToListAsync();
        }
    }
}