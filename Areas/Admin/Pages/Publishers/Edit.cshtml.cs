using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Areas.Admin.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _UW;
        public EditModel(IUnitOfWork UW)
        {
            _UW = UW;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var IsExitPublisher = await _UW.BaseRepository<Publisher>().FindByIDAsync(id);
            if(IsExitPublisher!=null)
            {
                Publisher= await _UW.BaseRepository<Publisher>().FindByIDAsync(id);
                return Page();
            }

            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            else
            {
                var Publishers = await _UW.BaseRepository<Publisher>().FindByIDAsync(Publisher.PublisherID);
                if(Publishers!=null)
                {
                    Publishers.PublisherName = Publisher.PublisherName;
                    await _UW.Commit();
                    return RedirectToPage("./Index");
                }

                else
                {
                    return NotFound();
                }
            }
        }
    }
}