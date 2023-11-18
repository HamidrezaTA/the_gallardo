using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> includeExpression = null);
        IQueryable<TEntity> GetAllAsQuery();
        IQueryable<TEntity> GetAsNoTracking(List<Expression<Func<TEntity, object>>> includeExpression = null);
        Task<TEntity> GetByIdAsync(long id, List<Expression<Func<TEntity, object>>> includeExpression = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<TEntity> UpdateAsync(TPrimaryKey id, TEntity entity);
        Task SoftDeleteAsync(long id);
        Task HardDeleteAsync(long id);
    }
}