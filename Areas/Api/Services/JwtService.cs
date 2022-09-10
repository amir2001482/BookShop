using BookShop.Areas.Admin.Data;
using BookShop.Areas.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        public JwtService(IApplicationUserManager userManager , IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> GeneratTokenAsync(ApplicationUser user)
        {
            var SecretKey = Encoding.UTF8.GetBytes("0123456789ALMTU@"); // key for signing jwt token
            var encryptKey = Encoding.UTF8.GetBytes("0123456789zxcvbn"); // key for signing payload of jwt token
            var SigningCredential = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature);
            var encryptingCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var SecurityTokenDes = new SecurityTokenDescriptor()
            {
                Issuer = "Pangerh-No.com",
                Audience = "Pangerh-No.com",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = SigningCredential,
                Subject = new ClaimsIdentity(await GetClaimsAsync(user)),
                EncryptingCredentials = encryptingCredential,
            };

            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = JwtTokenHandler.CreateToken(SecurityTokenDes);
            return JwtTokenHandler.WriteToken(securityToken);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
                new Claim("SecurityStampClaimType" , user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                
            };

            var Roles =  _roleManager.Roles.ToList();
             
            foreach (var item in Roles)
            {

                var RoleClaims = await _roleManager.FindClaimsInRole(item.Id);
                foreach(var claim in RoleClaims.Claims)
                {
                    Claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, claim.ClaimValue));
                }
            }
            foreach(var item in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item.Name));
            }
              

            return Claims;
        }

    }
}
