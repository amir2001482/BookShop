using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly IUnitOfWork _UW;
        public BooksApiController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        [HttpGet]
        public List<BooksIndexViewModel> GetBooks()
        {
            return _UW.BooksRepository.GetAllBooks("", "", "", "", "", "", "");
        }

        [HttpPost]
        public async Task<string> CreateBook(BooksCreateEditViewModel ViewModel)
        {
            if (await _UW.BooksRepository.CreateBookAsync(ViewModel))
                return "عملیات با موفقیت انجام شد.";
            else
                return "در انجام عملیات خطایی رخ داده است.";
        }

        [HttpPut]
        public async Task<string> EditBook(BooksCreateEditViewModel ViewModel)
        {
            if (await _UW.BooksRepository.EditBookAsync(ViewModel))
                return "ذخیره تغییرات با موفقیت انجام شد.";
            else
                return "در انجام عملیات خطایی رخ داده است.";
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var Book = await _UW.BaseRepository<Book>().FindByIDAsync(id);
            if (Book != null)
            {
                Book.Delete = true;
                await _UW.Commit();
                return Content("حذف کتاب با موفقیت انجام شد.");
            }

            else
            {
                return NotFound();
            }
        }
    }
}