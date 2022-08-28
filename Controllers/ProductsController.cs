using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class ProductsController : Controller
    {

        //[Authorize(Policy = "Atleast18")]
        public IActionResult BannedBooks()
        {
            return View();
        }
    }
}