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
    public class CitiesController : Controller
    {
        private readonly IUnitOfWork _UW;

        public CitiesController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        public async Task<IActionResult> Index(int id,int page = 1, int row = 10) // id==> ProvinceID
        {
            // Explicit Loading
            var Province = _UW._Context.Provices.SingleAsync(p => p.ProvinceID == id);
            _UW._Context.Entry(await Province).Collection(c => c.City).Load();
            return View(Province.Result);
        }


        public IActionResult Create(int id)
        {
            City city = new City(){ ProvinceID = id};
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityID,CityName,ProvinceID")] City city)
        {
            if (ModelState.IsValid)
            {
                Random rdm = new Random();
                int RandomID = rdm.Next(400, 1000);
                var ExitID = await _UW.BaseRepository<City>().FindByIDAsync(RandomID);
                while (ExitID != null)
                {
                    RandomID = rdm.Next(400, 1000);
                    ExitID = await _UW.BaseRepository<City>().FindByIDAsync(RandomID);
                }

                City City = new City() { CityID = RandomID, CityName = city.CityName,ProvinceID=city.ProvinceID };
                await _UW.BaseRepository<City>().CreateAsync(City);
                await _UW.Commit();
                return RedirectToAction(nameof(Index),new {id= city.ProvinceID});
            }
            return View(city);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _UW.BaseRepository<City>().FindByIDAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,CityName,ProvinceID")] City city)
        {
            if (id != city.CityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UW.BaseRepository<City>().Update(city);
                    await _UW.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _UW.BaseRepository<Provice>().FindByIDAsync(city.CityID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = city.ProvinceID });
            }
            return View(city);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _UW.BaseRepository<City>().FindByIDAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var City = await _UW.BaseRepository<City>().FindByIDAsync(id);
            if (City == null)
            {
                return NotFound();
            }

            else
            {
                _UW.BaseRepository<City>().Delete(City);
                await _UW.Commit();
                return RedirectToAction(nameof(Index), new { id = City.ProvinceID });
            }
        }     
    }
}