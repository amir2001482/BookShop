using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookSope2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AoutherController : Controller
    {
       
        private readonly IUnitOfWork _UW;
        public AoutherController(IUnitOfWork UW)
        {
            
            _UW = UW;
           
        }
        // اکشن متدی برای نامیش اطلاعات تمام نویسن
        public async Task<IActionResult> Index( int page = 1, int row = 5)
        {
          
            var Aouther = await _UW.BaseRepository<Author>().ReadAllAsync();
            var PagingModel = PagingList.Create(Aouther, row, page);
            PagingModel.RouteValue = new RouteValueDictionary()
            {
                {"row",row}
            };

            return View(PagingModel);
        }
        public async Task<IActionResult> AoutherBook(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var Aouther = await _UW.BaseRepository<Author>().ReadByIdAsync(Id); 
            return View(Aouther);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Aouther = _UW.BaseRepository<Author>().ReadByIdAsync(Id);
            _UW.BaseRepository<Author>().Delete(await Aouther);
            await _UW.Commit();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var Aouther = await _UW.BaseRepository<Author>().ReadByIdAsync(Id);
            return View(Aouther);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _UW.BaseRepository<Author>().Update(author);
                await _UW.Commit();
                ViewBag.MsgSuc = "عملیات با موفقیت انجام شد";
                return View(author);
            }
            else
            {
                ViewBag.MsgFail = "اطلاعات فرم نا معتبر است";
                return View(author);
            }
           
        }
        
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,FirstName,LastName")] Author author)
        {
                await _UW.BaseRepository<Author>().Create(author);
                await _UW.Commit();
                return RedirectToAction("Index");           
        }
    }
}
