using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SampleController : Controller
    {
        private readonly BookShopContext _context;

        public SampleController(BookShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Stopwatch Sw = new Stopwatch();
            Sw.Start();

            var Query = EF.CompileAsyncQuery((BookShopContext Context, int id)
                => Context.Books.SingleOrDefault(b => b.BookID == id));


            for (int i=0;i<1000;i++)
            {
                //var Book = _context.Books.SingleOrDefault(b => b.BookID == i);
                var Book = Query(_context, i);
            }

            Sw.Stop();

            return View(Sw.ElapsedMilliseconds);
        }
    }
}