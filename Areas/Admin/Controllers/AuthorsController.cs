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
        private readonly string NotFoundMessage ="چنین نویسنده ای وجود ندارد.";

        public AuthorsController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        //[ActionName("Author-Index")]
        public async Task<IActionResult> Index(int page = 1, int row = 10)
        {
            var Authors = _UW.BaseRepository<Author>().FindAllAsync();
            var PagingModel = PagingList.Create(await Authors, row, page);
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row},
            };

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
                return PartialView("_AuthorsTable", PagingModel);


            return View("Index",PagingModel);

        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Author>().CreateAsync(author);
                await _UW.Commit();
                TempData["notification"] = "درج اطلاعات با موفقیت انجام شد.";
            }
            return PartialView("_Create", author);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                ModelState.AddModelError(string.Empty, NotFoundMessage);

            var author = await _UW.BaseRepository<Author>().FindByIDAsync(id);

            if (author == null)
                ModelState.AddModelError(string.Empty, NotFoundMessage);
            return PartialView("_Edit", author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,FirstName,LastName")] Author author)
        {

            if (id != author.AuthorID)
                ModelState.AddModelError(string.Empty, NotFoundMessage);

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<Author>().Update(author);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _UW.BaseRepository<Author>().FindByIDAsync(author.AuthorID) == null)
                        return NotFound();
                    else
                        throw;
                        
                }
                TempData["notification"] = "ویرایش اطلاعات با موفقیت انجام شد.";
            }
            return PartialView("_Edit", author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                ModelState.AddModelError(string.Empty, NotFoundMessage);
            var author = await _UW.BaseRepository<Author>().FindByIDAsync(id);
            if (author == null)
            {
                ModelState.AddModelError(string.Empty, NotFoundMessage);
                return NotFound();
            }

            return PartialView("_Delete", author);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var Author= await _UW.BaseRepository<Author>().FindByIDAsync(id);
            if(Author==null)
                return NotFound();

            else
            {
                _UW.BaseRepository<Author>().Delete(Author);
                await _UW.Commit();
                TempData["notification"] = "حذف اطلاعات با موفقیت انجام شد.";
                return PartialView("_Delete", Author);
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

        public IActionResult Notifacation()
        {
            return PartialView("_Notifacation",TempData["notification"]);
        }
    }
}
