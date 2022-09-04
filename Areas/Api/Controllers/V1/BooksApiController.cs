using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Api.Class;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiResultFilter]
    public class BooksApiController : ControllerBase
    {
        private readonly IUnitOfWork _UW;
        public BooksApiController(IUnitOfWork UW)
        {
            _UW = UW;
        }

        [HttpGet]
        public ApiResult<List<BooksIndexViewModel>> GetBooks()
        {
            return Ok(_UW.BooksRepository.GetAllBooks("", "", "", "", "", "", ""));
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreateBook(BooksCreateEditViewModel ViewModel)
        {
            if (await _UW.BooksRepository.CreateBookAsync(ViewModel))
                return Ok("عملیات با موفقیت انجام شد.");
            else
                return BadRequest("در انجام عملیات خطایی رخ داده است");
        }

        [HttpPut]
        public async Task<ApiResult<string>> EditBook(BooksCreateEditViewModel ViewModel)
        {
            if (await _UW.BooksRepository.EditBookAsync(ViewModel))
                return Ok("ذخیره تغییرات با موفقیت انجام شد");
            else
                return BadRequest("در انجام عملیات خطایی رخ داده است");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteBook(int id)
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