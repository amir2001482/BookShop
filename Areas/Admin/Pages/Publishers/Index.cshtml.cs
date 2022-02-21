using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookSope2.Models;
using BookShop.Models.UnitOfWork;
using BookShop.Models;

namespace BookShop.Areas.Admin.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _UW;
        public IndexModel(IUnitOfWork UW)
        {
            _UW = UW;

        }
        //این پراپرتی ها برای صفحه بندی تعریف شده اند
        [BindProperty(SupportsGet =true)]
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 3;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowProvios => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;

        public IEnumerable<Publisher> publishers { get; set; }
        public async Task<IActionResult> OnGet()
        {
            publishers = await _UW.BaseRepository<Publisher>().ReadAllAsync();
            Count = await _UW.BaseRepository<Publisher>().Count();
            publishers = await _UW.BaseRepository<Publisher>().GetPaginateResultasync(CurrentPage, PageSize);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var Publisher = await _UW.BaseRepository<Publisher>().ReadByIdAsync(Id);
            _UW.BaseRepository<Publisher>().Delete(Publisher);
            await _UW.Commit();
            return RedirectToPage("./Index");
        }
    }
}
