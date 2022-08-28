using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public class RepositoryBase<TEntity,TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext : DbContext
    {
        protected TContext _Context { get; set; }
        private DbSet<TEntity> dbSet;
        public RepositoryBase(TContext Context)
        {
            _Context = Context;
            dbSet = _Context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public async Task<TEntity> FindByIDAsync(Object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity,bool>> filter=null,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,params Expression<Func<TEntity,object>>[] includes)
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            foreach(var include in includes)
            {
                query = query.Include(include);
            }

            if(filter!=null)
            {
                query = query.Where(filter);
            }

            if(orderBy!=null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity) => dbSet.Update(entity);

        public void Delete(TEntity entity) => dbSet.Remove(entity);

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);

        public void UpdateRange(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);

        public void DeleteRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

        public async Task<List<TEntity>> GetPaginateResultAsync(int CurrentPage,int PageSize=1)
        {
            var Entities = await FindAllAsync();
            return Entities.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }

        public int GetCount()
        {
            return dbSet.Count();
        }
    }
}
