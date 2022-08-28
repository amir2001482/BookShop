using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TranslatorsController : Controller
    {
        private readonly IUnitOfWork _UW;

        public TranslatorsController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        public async Task<IActionResult> Index(int page = 1, int row = 10)
        {
            var Translators = _UW.BaseRepository<Translator>().FindAllAsync();
            var PagingModel = PagingList.Create(await Translators, row, page);
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
        public async Task<IActionResult> Create([Bind("TranslatorID,Name,Family")] Translator translator)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Translator>().CreateAsync(translator);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(translator);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _UW.BaseRepository<Translator>().FindByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TranslatorID,Name,Family")] Translator translator)
        {
            if (id != translator.TranslatorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<Translator>().Update(translator);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _UW.BaseRepository<Translator>().FindByIDAsync(translator.TranslatorID) == null)
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
            return View(translator);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translator = await _UW.BaseRepository<Translator>().FindByIDAsync(id);
            if (translator == null)
            {
                return NotFound();
            }

            return View(translator);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Translator = await _UW.BaseRepository<Translator>().FindByIDAsync(id);
            if (Translator == null)
            {
                return NotFound();
            }

            else
            {
                _UW.BaseRepository<Translator>().Delete(Translator);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}