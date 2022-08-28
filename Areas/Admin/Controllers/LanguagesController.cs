using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using System.Data.SqlClient;
using BookShop.Models.UnitOfWork;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguagesController : Controller
    {
        private readonly IUnitOfWork _UW;

        public LanguagesController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        public async Task<IActionResult> Index(int page = 1, int row = 10)
        {
            var Languages = _UW.BaseRepository<Language>().FindAllAsync();
            var PagingModel = PagingList.Create(await Languages, row, page);
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row},
            };
            return View(PagingModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageID,LanguageName")] Language language)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Language>().CreateAsync(language);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _UW.BaseRepository<Language>().FindByIDAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageID,LanguageName")] Language language)
        {
            if (id != language.LanguageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<Language>().Update(language);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.LanguageID))
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
            return View(language);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _UW.BaseRepository<Language>().FindByIDAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            else
            {
                var Language = _UW.BaseRepository<Language>().FindByIDAsync(id);
                if (Language==null)
                {
                    return NotFound();
                }
                else
                {
                    object[] Parameters = { new SqlParameter("@id", id) };
                    await _UW._Context.Database.ExecuteSqlCommandAsync("delete from dbo.Languages where(LanguageID=@id)", Parameters);
                    return RedirectToAction(nameof(Index));
                }         
            }
           
        }

        private bool LanguageExists(int id)
        {
            return _UW._Context.Languages.Any(e => e.LanguageID == id);
        }
    }
}
