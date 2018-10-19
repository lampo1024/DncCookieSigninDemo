using DncCookieSignin.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DncCookieSignin.Pages.Account
{
    public class RefreshModel : PageModel
    {
        private readonly UserManagerService _userManagerService;
        public RefreshModel(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        public IActionResult OnGet()
        {
            _userManagerService.Refresh(HttpContext, UserContextService.GetUser);
            return RedirectToPage("/account/profile");
        }
    }
}