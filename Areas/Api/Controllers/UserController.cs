using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.Repository;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IConvertDate _convertDate;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUserRepository _userRepository;
        public UserController(IApplicationUserManager userManager , IConvertDate convertDate , IApplicationRoleManager roleManager , IUserRepository userRepository)
        {
            _userManager = userManager;
            _convertDate = convertDate;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<List<UsersViewModel>> GetAllUser()
        {
            return await _userManager.GetAllUsersWithRolesAsync();
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user =  await _userManager.FindUserWithRolesByIdAsync(userId);
            if (user == null)
                return NotFound();
            return new JsonResult(user);
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody]RegisterBaseViewModel viewModel)
        {
            var result = await _userRepository.RegisterAsync(viewModel);
            if (result.Succeeded)
                return new JsonResult("عملیات با موفقیت انجام شد");
            else
                return new JsonResult(result.Errors);



        }
    }
}
