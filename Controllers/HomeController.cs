using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookSope2.Models;
using System.Security.Claims;

namespace BookSope2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string Id)
        {
            //var userInfo = User;
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            //var userName = User.FindFirst(ClaimTypes.Name);
            //var userRole = User.FindFirst(ClaimTypes.Role);
            if (Id !=null)
                ViewBag.Msg = "ایمیل با موفقیت برای شما ارسال شد لطفا آنرا تایید کنیید.";
            return View();
        }

        

       

       
    }
}
