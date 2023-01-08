using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookShop.Models.UnitOfWork;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly IUnitOfWork _UW;

        public AuthorsController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        //[ActionName("Author-Index")]
        public async Task<IActionResult> Index(int page = 1, int row = 10)
        {
            var Authors = _UW.BaseRepository<Author>().FindAllAsync();

            //var Authors = _UW.BaseRepository<Author>().FindByConditionAsync(o => o.FirstName.Contains("آرزو"),o=>o.OrderByDescending(k=>k.LastName),o=>o.Author_Books);
            var PagingModel = PagingList.Create(await Authors, row, page);
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row},
            };
            return View("Index",PagingModel);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Author>().CreateAsync(author);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _UW.BaseRepository<Author>().FindByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,FirstName,LastName")] Author author)
        {
            if (id != author.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<Author>().Update(author);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _UW.BaseRepository<Author>().FindByIDAsync(author.AuthorID)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _UW.BaseRepository<Author>().FindByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Author= await _UW.BaseRepository<Author>().FindByIDAsync(id);
            if(Author==null)
            {
                return NotFound();
            }

            else
            {
                _UW.BaseRepository<Author>().Delete(Author);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
        }


        private List<EntityStates> DisplayStates(IEnumerable<EntityEntry> entities)
        {
            List<EntityStates> EntityStates = new List<EntityStates>();
            foreach(var entry in entities)
            {
                EntityStates states = new EntityStates()
                {
                    EntityName = entry.Entity.GetType().Name,
                    EntityState=entry.State.ToString(),
                };

                EntityStates.Add(states);
            }

            return EntityStates;
        }

        public async Task<IActionResult> AuthorBooks(int id)
        {

            // Enable LazyLoading 
            var Author = _UW.BaseRepository<Author>().FindByIDAsync(id);
            if(Author==null)
            {
                return NotFound();
            }

            else
            {
                return View(await Author);
            }
          
        }
    }
}
