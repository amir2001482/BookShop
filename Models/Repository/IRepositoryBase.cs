using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> ReadAllAsync();
        IEnumerable<TEntity> ReadAll();
        Task<TEntity> ReadByIdAsync(object Id);
        TEntity ReadbyId(object Id);
        Task<IEnumerable<TEntity>> ReadByCondition(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null, params Expression<Func<TEntity, object>>[] Include);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task CreateRange(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<List<TEntity>> GetPaginateResultasync(int CurrentPage, int PageSize);
        Task<int> Count();
    }
}
