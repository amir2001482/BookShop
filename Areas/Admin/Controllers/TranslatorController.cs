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
    public class TranslatorController : Controller
    {
        private readonly IUnitOfWork _UW;
        public TranslatorController(IUnitOfWork UW)
        {
            _UW = UW;
        }
        public async Task<IActionResult> Index(int page=1,int row = 5)
        {
            var Translators = await _UW.BaseRepository<Translator>().ReadAllAsync();
            var pagingModel = PagingList.Create(Translators, row, page);
            pagingModel.RouteValue = new RouteValueDictionary()
            {
                {"row",row}
            };
            return View(pagingModel);
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Translator translator)
        {
            if (ModelState.IsValid)
            {
                await _UW.BaseRepository<Translator>().Create(translator);
                await _UW.Commit();
                return RedirectToAction("Index");
            }
            return NotFound();
            

        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Translator = await _UW.BaseRepository<Translator>().ReadByIdAsync(Id);
            _UW.BaseRepository<Translator>().Delete(Translator);
            await _UW.Commit();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var Translator = await _UW.BaseRepository<Translator>().ReadByIdAsync(Id);
            if (Translator==null)
            {
                return NotFound();
            }
            return View(Translator);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Translator translator)
        {
            if (ModelState.IsValid)
            {
                 _UW.BaseRepository<Translator>().Update(translator);
                await _UW.Commit();
                ViewBag.MsgSuc = "عملیات با موفقیت انجام شد";
                return View();
            }
            ViewBag.MsgFail = "اطلاعات فرم نا معتبر است";
            return View(translator);
        }
    }
}
