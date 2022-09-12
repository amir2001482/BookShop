using BookShop.Areas.Admin.Data;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
    public class JwtService : IJwtService // only for generat jwt token
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly SiteSettings _siteSettings;
        public JwtService(IApplicationUserManager userManager , IApplicationRoleManager roleManager , IOptionsSnapshot<SiteSettings> sitesettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _siteSettings = sitesettings.Value;
        }
        public async Task<string> GeneratTokenAsync(ApplicationUser user)
        {
            var SecretKey = Encoding.UTF8.GetBytes(_siteSettings.jwtSettings.Secretkey); // key for signing jwt token
            var encryptKey = Encoding.UTF8.GetBytes(_siteSettings.jwtSettings.EncryptKey); // key for signing payload of jwt token
            var SigningCredential = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature);
            var encryptingCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var SecurityTokenDes = new SecurityTokenDescriptor()
            {
                Issuer = _siteSettings.jwtSettings.Issuer,
                Audience = _siteSettings.jwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.jwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSettings.jwtSettings.ExpirationMinutes),
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
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType , user.SecurityStamp),
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
