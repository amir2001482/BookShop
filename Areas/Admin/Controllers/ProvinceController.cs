using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookSope2.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvinceController : Controller
    {
        
        private readonly IUnitOfWork _UW ;
        public ProvinceController(IUnitOfWork UW )
        {
            _UW = UW;
           
        }
        public async Task<IActionResult> Index()
        {
            var Perovinces = await _UW.BaseRepository<Provice>().ReadAllAsync();
            return View(Perovinces);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var province = await _UW.BaseRepository<Provice>().ReadByIdAsync(Id);
            if (province==null)
            {
                return NotFound();
            }
            return View(province);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Provice provice)
        {
            if (ModelState.IsValid)
            {
                _UW.BaseRepository<Provice>().Update(provice);
                await _UW.Commit();
                ViewBag.MSGSuc = "عملیات با موفقیت انجام شد";
                return View();


            }
            else
            {
                ViewBag.MSGFail = "اطلاعات فرم نا معتبر است ";
                return View(provice);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Provice provice)
        {
           
            
                Provice provice1 = new Provice() { City = null, ProvinceName= provice.ProvinceName };
                await _UW.BaseRepository<Provice>().Create(provice1);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            
           

        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var province = await _UW.BaseRepository<Provice>().ReadByIdAsync(Id);
            if (province==null)
            {
                return NotFound();
            }
            _UW.BaseRepository<Provice>().Delete(province);
            await _UW.Commit();
            return RedirectToAction("Index");
        }


    }
}
