using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvincesController : Controller
    {
        private readonly IUnitOfWork _UW;

        public ProvincesController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        public async Task<IActionResult> Index(int page = 1, int row = 10)
        {
            var Provinces = _UW.BaseRepository<Provice>().FindAllAsync();
            var PagingModel = PagingList.Create(await Provinces, row, page);
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
        public async Task<IActionResult> Create([Bind("ProvinceID,ProvinceName")] Provice province)
        {
            if (ModelState.IsValid)
            {
                Random rdm = new Random();
                int RandomID = rdm.Next(32, 1000);
                var ExitID =await _UW.BaseRepository<Provice>().FindByIDAsync(RandomID);
                while(ExitID!=null)
                {
                    RandomID = rdm.Next(32, 1000);
                    ExitID = await _UW.BaseRepository<Provice>().FindByIDAsync(RandomID);
                }

                Provice Province=new Provice(){ProvinceID=RandomID,ProvinceName= province.ProvinceName};
                await _UW.BaseRepository<Provice>().CreateAsync(Province);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _UW.BaseRepository<Provice>().FindByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProvinceID,ProvinceName")] Provice province)
        {
            if (id != province.ProvinceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<Provice>().Update(province);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _UW.BaseRepository<Provice>().FindByIDAsync(province.ProvinceID) == null)
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
            return View(province);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _UW.BaseRepository<Provice>().FindByIDAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Province = await _UW.BaseRepository<Provice>().FindByIDAsync(id);
            if (Province == null)
            {
                return NotFound();
            }

            else
            {
                _UW.BaseRepository<Provice>().Delete(Province);
                await _UW.Commit();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}