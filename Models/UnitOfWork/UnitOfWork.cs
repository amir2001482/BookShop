using BookShop.Classes;
using BookShop.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public BookShopContext _Context { get; }
        private IBooksRepository _IBooksRepository;
        private readonly IConvertDate _convertDate;
        public UnitOfWork(BookShopContext Context, IConvertDate convertDate)
        {
            _Context = Context;
            _convertDate = convertDate;
        }

        public IRepositoryBase<TEntity> BaseRepository<TEntity>() where TEntity : class
        {
            IRepositoryBase<TEntity> repository = new RepositoryBase<TEntity, BookShopContext>(_Context);
            return repository;
        }

        public IBooksRepository BooksRepository
        {
            get
            {
                if(_IBooksRepository==null)
                {
                    _IBooksRepository = new BooksRepository(this,_convertDate);
                }

                return _IBooksRepository;
            }
        }

        public async Task Commit()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
