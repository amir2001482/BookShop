using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public interface IApplicationUserManager
    {
        #region BaseClass
        IPasswordHasher<ApplicationUser> PasswordHasher { get; set; }
        IList<IUserValidator<ApplicationUser>> UserValidators { get; }
        IList<IPasswordValidator<ApplicationUser>> PasswordValidators { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IdentityOptions Options { get; set; }
        bool SupportsUserAuthenticationTokens { get; }
        bool SupportsUserAuthenticatorKey { get; }
        bool SupportsUserTwoFactorRecoveryCodes { get; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<ApplicationUser> Users { get; }
        Task<string> GenerateConcurrencyStampAsync(ApplicationUser user);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        string NormalizeKey(string key);
        Task UpdateNormalizedUserNameAsync(ApplicationUser user);
        Task<string> GetUserNameAsync(ApplicationUser user);
        Task<IdentityResult> SetUserNameAsync(ApplicationUser user, string userName);
        Task<string> GetUserIdAsync(ApplicationUser user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> HasPasswordAsync(ApplicationUser user);
        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(ApplicationUser user);
        Task<string> GetSecurityStampAsync(ApplicationUser user);
        Task<IdentityResult> UpdateSecurityStampAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey);
        Task<IdentityResult> AddLoginAsync(ApplicationUser user, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user);
        Task<IdentityResult> AddClaimAsync(ApplicationUser user, Claim claim);
        Task<IdentityResult> AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
        Task<IdentityResult> ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim);
        Task<IdentityResult> RemoveClaimAsync(ApplicationUser user, Claim claim);
        Task<IdentityResult> RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<string> GetEmailAsync(ApplicationUser user);
        Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task UpdateNormalizedEmailAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
        Task<string> GenerateChangeEmailTokenAsync(ApplicationUser user, string newEmail);
        Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token);
        Task<string> GetPhoneNumberAsync(ApplicationUser user);
        Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUser user, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(ApplicationUser user);
        Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUser user, string phoneNumber);
        Task<bool> VerifyChangePhoneNumberTokenAsync(ApplicationUser user, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose, string token);
        Task<string> GenerateUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose);
        void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<ApplicationUser> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUser user);
        Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string tokenProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(ApplicationUser user, string tokenProvider);
        Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user);
        Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled);
        Task<bool> IsLockedOutAsync(ApplicationUser user);
        Task<IdentityResult> SetLockoutEnabledAsync(ApplicationUser user, bool enabled);
        Task<bool> GetLockoutEnabledAsync(ApplicationUser user);
        Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user);
        Task<IdentityResult> SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(ApplicationUser user);
        Task<IdentityResult> ResetAccessFailedCountAsync(ApplicationUser user);
        Task<int> GetAccessFailedCountAsync(ApplicationUser user);
        Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim);
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        Task<string> GetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName);
        Task<IdentityResult> SetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName, string tokenValue);
        Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName);
        Task<string> GetAuthenticatorKeyAsync(ApplicationUser user);
        Task<IdentityResult> ResetAuthenticatorKeyAsync(ApplicationUser user);
        string GenerateNewAuthenticatorKey();
        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(ApplicationUser user, int number);
        Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(ApplicationUser user, string code);
        Task<int> CountRecoveryCodesAsync(ApplicationUser user);
        Task<byte[]> CreateSecurityTokenAsync(ApplicationUser user);

        #endregion

        #region CustomMethod
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<UsersViewModel>> GetAllUsersWithRolesAsync();
        Task<UsersViewModel> FindUserWithRolesByIdAsync(string UserID);
        Task<string> GetFullName(ClaimsPrincipal User);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal User);
        #endregion
    }
}
