using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BookShop.Areas.Admin.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _UW;
        public IndexModel(IUnitOfWork UW)
        {
            _UW = UW;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int Count { get; set; }
        public int PageSize { get; set; } = 5;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public IEnumerable<Publisher> Publishers { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Count = _UW.BaseRepository<Publisher>().GetCount();
            Publishers =await _UW.BaseRepository<Publisher>().GetPaginateResultAsync(CurrentPage,PageSize);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if(id==null)
                return NotFound();

            else
            {
                var Publisher = await _UW.BaseRepository<Publisher>().FindByIDAsync(id);
                if(Publisher==null)
                {
                    return NotFound();
                }

                else
                {
                    _UW.BaseRepository<Publisher>().Delete(Publisher);
                    await _UW.Commit();
                    return RedirectToPage("./Index");
                }
            }
        }

        public async Task<IActionResult> OnPostInsertAsync([FromBody]Publisher model)
        {
            await _UW.BaseRepository<Publisher>().CreateAsync(model);
            await _UW.Commit();
            // baray pagination ba ajax ien parametr ha ro ersal mikonim.
            return new JsonResult(JsonConvert.SerializeObject(  new { publishers = await _UW.BaseRepository<Publisher>().GetPaginateResultAsync(CurrentPage, PageSize), currentPage = CurrentPage , totalPages = TotalPages }));
        }
    }
}