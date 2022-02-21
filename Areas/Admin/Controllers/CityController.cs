using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookSope2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSope2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private IUnitOfWork _UW { get; }
        public CityController( IUnitOfWork UW)
        {
            _UW = UW;

        }
        public async Task<IActionResult> Index(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            //استفاده از قابلیت Ewp;icit loading 
             var Perovince = await _UW.BaseRepository<Provice>().ReadByIdAsync(Id);
            //قرار دادن یک فیلتر بر سر دیتا ها به هنگام عملیات بارگذاری
            //_context.Entry(Perovince).Collection(c => c.City).Query().Where(a => a.CityName.Contains("مشهد")).Load();

            return View(Perovince);
        }
        public  IActionResult Create(int Id)
        {
            City city = new City() { ProvinceID = Id };
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            
            
                City city1 = new City() { CityName = city.CityName, ProvinceID = city.ProvinceID };
                await _UW.BaseRepository<City>().Create(city1);
                await _UW.Commit();
                return RedirectToAction(nameof(Index), new { id = city.ProvinceID });




        }
        public async Task<IActionResult>  Edit(int Id)
        {
            var city = await _UW.BaseRepository<City>().ReadByIdAsync(Id);
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City city)
        {
            if (ModelState.IsValid)
            {
                City city1 = new City() {  CityID = city.CityID ,CityName = city.CityName, ProvinceID = city.ProvinceID };
                _UW.BaseRepository<City>().Update(city1);
                await _UW.Commit();
                ViewBag.MSGSuc = "اطلاعات با موفقیت ثبت شد ";
                return View();
            }
            ViewBag.MsgFail = "اطلاعات فرم نا معتبر است ";
            return View(city);


        }
        public async Task<IActionResult> Delete(int Id)
        {
            var city = await _UW.BaseRepository<City>().ReadByIdAsync(Id);
            _UW.BaseRepository<City>().Delete(city);
            await _UW.Commit();
            return RedirectToAction(nameof(Index), new { id = city.ProvinceID });

        }
    }
}
