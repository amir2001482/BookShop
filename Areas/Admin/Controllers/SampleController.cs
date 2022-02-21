using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookSope2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    // این کنترولر فقط برای بررسی سرعت کوئری ها با قابلیت Compile Query و مقاسیه آن با حالت معمولی است
    public class SampleController : Controller
    {


        private readonly BookShopContext _context;

        public SampleController(BookShopContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            //تعریف یک کوئری با کمک خاصیت CompileQuery
            var Query = EF.CompileQuery((BookShopContext Context, int Id) =>
            Context.Books.SingleOrDefault(b => b.BookID == Id));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i<100; i++)
            {
                //استفاده از دو کوئری 

                //var Book = _context.Books.SingleOrDefault(b => b.BookID == i);
                var Book = Query(_context, i);
            }
            sw.Stop();
            return View(sw.ElapsedMilliseconds);
        }
    }
}
