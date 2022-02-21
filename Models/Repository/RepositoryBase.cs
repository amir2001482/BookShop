using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public class RepositoryBase<TEntity,TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext:DbContext
    {
        protected TContext _context { get; set; }
        private DbSet<TEntity> dbset;
        public RepositoryBase(TContext context)
        {
            _context = context;
            dbset = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            return await dbset.AsNoTracking().ToListAsync();
        }
        public IEnumerable<TEntity> ReadAll()
        {
            return dbset.AsNoTracking().ToList();
        }
        public async Task<TEntity> ReadByIdAsync(object Id)
        {
            return await dbset.FindAsync(Id);
        }
        public async Task<IEnumerable<TEntity>> ReadByCondition(Expression<Func<TEntity,bool>> Filter=null , Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> OrderBy=null, params Expression<Func<TEntity, object>>[] Include)
        {
            IQueryable<TEntity> Query = dbset;
            if (Filter!=null)
            {
                Query = Query.Where(Filter);
            }
            if (OrderBy!=null)
            {
                Query = OrderBy(Query);
            }
            foreach(var include in Include)
            {
                Query = Query.Include(include);
            }
            return await Query.ToListAsync();
        }
        public async Task Create(TEntity entity) => await dbset.AddAsync(entity);
        
           
        
        public void Update(TEntity entity)=> dbset.Update(entity);



        public void Delete(TEntity entity)=> dbset.Remove(entity);
        
          
        
        public async Task CreateRange(IEnumerable<TEntity> entities) => await dbset.AddRangeAsync(entities);
        
           
        
        public void UpdateRange(IEnumerable<TEntity> entities)=> dbset.UpdateRange(entities);
        
            
        
        public void DeleteRange(IEnumerable<TEntity> entities) => dbset.RemoveRange(entities);
        public TEntity ReadbyId(object Id)
        {
            return dbset.Find(Id);
        }
        //این متد برای صفحه بندی Razor Pages  تعریف شده است
        public async Task<List<TEntity>> GetPaginateResultasync(int CurrentPage , int PageSize)
        {
            var Entities = await ReadAllAsync();
            return Entities.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }
        //این متد برای گرفتن تعداد انتیتی های موجود در دیتابیس تعریف شده است 
        public async Task<int> Count()
        {
            return await dbset.CountAsync();
        }
        
           
        
    }
}
