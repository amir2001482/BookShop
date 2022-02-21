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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _UW;
        public EditModel(IUnitOfWork UW)
        {
            _UW = UW;

        }
        [BindProperty]
        public Publisher publisher { get; set; }
        public async Task<IActionResult> OnGet(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var publisherExist = await _UW.BaseRepository<Publisher>().ReadByIdAsync(Id);
            if (publisherExist == null)
            {
                return NotFound();
            }
            publisher = await _UW.BaseRepository<Publisher>().ReadByIdAsync(Id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var publishers = await _UW.BaseRepository<Publisher>().ReadByIdAsync(publisher.PublisherID);
            if (ModelState.IsValid)
            {
                publishers.PublisherName = publisher.PublisherName;
                
                await _UW.Commit();
                return RedirectToPage("./Index");
            }
            return Page();
        }



    }   

}
