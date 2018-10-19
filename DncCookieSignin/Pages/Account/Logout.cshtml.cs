using DncCookieSignin.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DncCookieSignin.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly UserManagerService _userManagerService;
        public LogoutModel(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        public IActionResult OnGet()
        {
            _userManagerService.SignOut(HttpContext);
            return RedirectToPage("/account/login");
        }
    }
}