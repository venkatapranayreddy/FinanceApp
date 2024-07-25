using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace api.Repository
{
    // public class AccountRepository : SignInManager<AppUser> , IAccountRepository
    // {
    //     public AccountRepository(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<AppUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<AppUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<AppUser> confirmation) :
    //      base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    //     {
    //     }

    //     public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    // {
    //     var result = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

    //     if (result.Succeeded)
    //     {
    //         var user = await UserManager.FindByNameAsync(userName);
    //         user.LastLoginTime = DateTime.UtcNow;
    //         await UserManager.UpdateAsync(user);
    //     }

    //     return result;
    // }
    // }
}