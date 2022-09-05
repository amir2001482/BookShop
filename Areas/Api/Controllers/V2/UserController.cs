using BookShop.Areas.Api.Class;
using BookShop.Areas.Api.Services;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.Repository;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers.V2
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class UserController : V1.UserController
    {
        public UserController(IApplicationUserManager userManager, IConvertDate convertDate, IApplicationRoleManager roleManager, IUserRepository userRepository , IJwtService jwtService)
            :base( userManager,  convertDate,  roleManager,  userRepository , jwtService)
        {

        }

        public override Task<ApiResult<string>> Register([FromBody] RegisterBaseViewModel viewModel)
        {
            return base.Register(viewModel);
        }
    }
}
