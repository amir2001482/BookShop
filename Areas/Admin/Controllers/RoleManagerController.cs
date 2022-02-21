using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleManagerController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        public RoleManagerController(IApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index(int Page=1 , int Row = 10)
        {
            var Roles = _roleManager.GetAllRolesAndUsersCount();
            var PagingModel = PagingList.Create(Roles, Row, Page);
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"Row", Row }

            };
            return View(PagingModel);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleManagerViewModel VM)
        {
            if (ModelState.IsValid)
            {
                var Result = await _roleManager.CreateAsync(new ApplicationRoles { Name = VM.RoleName , Description=VM.Description});
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(VM);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            if (Id==null)
                return NotFound();
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role == null)
                return NotFound();
            var RoleVM = new RoleManagerViewModel { Id = Role.Id, RoleName = Role.Name , Description = Role.Description };

            return View(RoleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleManagerViewModel VM)
        {
            if (ModelState.IsValid)
            {
                var Role = await _roleManager.FindByIdAsync(VM.Id);
                if (Role ==null)
                     return NotFound();
                Role.Name = VM.RoleName;
                Role.Description = VM.Description;
                var Result = await _roleManager.UpdateAsync(Role);
                if (Result.Succeeded)
                {
                    ViewBag.Success = "عملیات با موفقیت انجام شد ";
                    return View();
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                
            }
            return View(VM);
        }
        [HttpGet]
        public async Task<IActionResult>  DeleteRole(string Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(Id);
            var RoleVM = new RoleManagerViewModel { Id = Role.Id, RoleName = Role.Name , Description = Role.Description};
            return View(RoleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id==null)
            {
                return NotFound();
            }
            var Role = await _roleManager.FindByIdAsync(Id);
            var Result = await _roleManager.DeleteAsync(Role);
            if (Result.Succeeded==true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Error = "در حذف اطلاعات خطایی رخ داده است ";
            return View( new RoleManagerViewModel { Id = Role.Id , RoleName= Role.Name });
        }
    }
}
