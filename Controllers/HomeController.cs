using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookShop.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            //var UserInfo = User;
            //string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string Role = User.FindFirstValue(ClaimTypes.Role);

            if (id != null)
                ViewBag.ConfirmEmailAlert = "لینک فعال سازی حساب کاربری به ایمیل شما ارسال شد لطفا با کلیک روی این لینک حساب خود را فعال کنید.";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
