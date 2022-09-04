﻿using BookShop.Areas.Api.Class;
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
    [ApiResultFilter]
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
        public async Task<ApiResult<List<UsersViewModel>>> GetAllUser()
        {
            return Ok(await _userManager.GetAllUsersWithRolesAsync());
        }


        [HttpGet("{userId}")]
        public async Task<ApiResult<UsersViewModel>> GetUserById(string userId)
        {
            var user =  await _userManager.FindUserWithRolesByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ApiResult<string>> Register([FromBody]RegisterBaseViewModel viewModel)
        {
            var result = await _userRepository.RegisterAsync(viewModel);
            if (result.Succeeded)
                return Ok("عملیات با موفقیت انجام شد");
            else
                return BadRequest(result.Errors);



        }

        [HttpPost("sing-in")]
        public async Task<ApiResult<string>> SingIn([FromBody]SingInBaseViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
                return BadRequest("کاربری با این ایمیل یافت نشد");
            var result = await _userManager.CheckPasswordAsync(user, viewModel.Password);
            if (result)
                return Ok("احراز هویت با موفقیت انجام شد");
            else
                return BadRequest("نام کاربری یا کلمه عبور شما صحیح نمی باشد");
        }
    }
}
