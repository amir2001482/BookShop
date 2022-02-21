using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookSope2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class langugeController : Controller
    {
        private readonly IUnitOfWork _UW;
        public langugeController(IUnitOfWork UW)
        {
            _UW = UW;
        }
        public async Task<IActionResult> Index(int page = 1, int row = 5)
        {
            var languages = await _UW.BaseRepository<Language>().ReadAllAsync();
            var pagingModel = PagingList.Create(languages, row, page);
            pagingModel.RouteValue = new RouteValueDictionary()
            {
                {"row",row}
            };
            return View(pagingModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Language>().Create(language);
                await _UW.Commit();
                return RedirectToAction("Index");
            }
            return NotFound();


        }
        public async Task<IActionResult> Delete(int Id)
        {
            var language = await _UW.BaseRepository<Language>().ReadByIdAsync(Id);
            _UW.BaseRepository<Language>().Delete(language);
            await _UW.Commit();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var language = await _UW.BaseRepository<Language>().ReadByIdAsync(Id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                _UW.BaseRepository<Language>().Update(language);
                await _UW.Commit();
                ViewBag.MsgSuc = "عملیات با موفقیت انجام شد";
                return View();
            }
            ViewBag.MsgFail = "اطلاعات فرم نا معتبر است";
            return View(language);
        }
    }
}
