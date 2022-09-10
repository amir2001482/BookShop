using BookShop.Areas.Admin.Data;
using BookShop.Areas.Api.Class;
using BookShop.Areas.Api.Services;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.Repository;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiResultFilter]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IConvertDate _convertDate;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public UserController(IApplicationUserManager userManager , IConvertDate convertDate , IApplicationRoleManager roleManager , IUserRepository userRepository , IJwtService jwtService)
        {
            _userManager = userManager;
            _convertDate = convertDate;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        [HttpGet]
        //[Authorize(Roles ="مدیر سایت")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public virtual async Task<ApiResult<List<UsersViewModel>>> GetAllUser()
        {
            var userName = HttpContext.User.Identity.Name;
            var phoneNumber = HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);
            return Ok(await _userManager.GetAllUsersWithRolesAsync());
        }


        [HttpGet("{userId}")]
        public virtual async Task<ApiResult<UsersViewModel>> GetUserById(string userId)
        {
            var user =  await _userManager.FindUserWithRolesByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("Register")]
        public virtual async Task<ApiResult<string>> Register([FromBody]RegisterBaseViewModel viewModel)
        {
            var result = await _userRepository.RegisterAsync(viewModel);
            if (result.Succeeded)
                return Ok("عملیات با موفقیت انجام شد");
            else
                return BadRequest(result.Errors);



        }

        [HttpPost("sing-in")]
        public virtual async Task<ApiResult<string>> SingIn([FromBody]SingInBaseViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
                return BadRequest("نام کاربری یا کلمه عبور شما صحیح نمی باشد");
            var result = await _userManager.CheckPasswordAsync(user, viewModel.Password);
            if (result)
                return Ok(await _jwtService.GeneratTokenAsync(user));
            else
                return BadRequest("نام کاربری یا کلمه عبور شما صحیح نمی باشد");
        }
    }
}
