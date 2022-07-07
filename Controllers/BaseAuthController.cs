using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VZ.MoneyFlow.API.Controllers
{
    [Authorize]   
    public class BaseAuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public BaseAuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;            
        }
        internal async Task<IdentityUser> GetUserAsync()
        {
            var userEmail = HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(userEmail);
            return user;
        }

        internal string GetUserId()
        {
            var userId = HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
