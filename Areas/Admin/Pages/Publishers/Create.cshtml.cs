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
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _UW;
        public CreateModel(IUnitOfWork UW)
        {
            _UW = UW;

        }
        [BindProperty]
        public Publisher publisher { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _UW.BaseRepository<Publisher>().Create(publisher);
            await _UW.Commit();
            return RedirectToPage("./Index");


        }
    }
}
