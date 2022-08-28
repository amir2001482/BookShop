using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Admin.Data;
using BookShop.Areas.Admin.Services;
using BookShop.Areas.Identity.Data;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DynamicAccessController : Controller
    {
        public readonly IApplicationRoleManager _roleManager;
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        public DynamicAccessController(IApplicationRoleManager roleManager, IMvcActionsDiscoveryService mvcActionsDiscovery)
        {
            _roleManager = roleManager;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
                return NotFound();

            var Role = await _roleManager.FindClaimsInRole(id);
            if (Role == null)
                return NotFound();

            var SecuredControllerActions = _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            return View(new DynamicAccessIndexViewModel
            {
                RoleIncludeRoleClaims = Role,
                SecuredControllerActions = SecuredControllerActions,
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DynamicAccessIndexViewModel ViewModel)
        {
            var Result = await _roleManager.AddOrUpdateClaimsAsync(ViewModel.RoleId, ConstantPolicies.DynamicPermissionClaimType, ViewModel.ActionIds);
            if (!Result.Succeeded)
                ModelState.AddModelError(string.Empty, "در حین انجام عملیات خطایی رخ داده است.");

            return RedirectToAction("Index",new {id=ViewModel.RoleId});
        }
    }
}