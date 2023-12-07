using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>?> GetAllAsync(List<Expression<Func<TEntity, object>>>? includeExpression = null);
        IQueryable<TEntity> GetAllAsQuery(List<Expression<Func<TEntity, object>>>? includeExpression = null);
        IQueryable<TEntity> GetAsNoTracking(List<Expression<Func<TEntity, object>>>? includeExpression = null);
        Task<TEntity?> GetByIdAsync(TPrimaryKey id, List<Expression<Func<TEntity, object>>>? includeExpression = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<TEntity> UpdateAsync(TPrimaryKey id, TEntity entity);
        Task SoftDeleteAsync(TPrimaryKey id);
        Task HardDeleteAsync(TPrimaryKey id);
    }
}