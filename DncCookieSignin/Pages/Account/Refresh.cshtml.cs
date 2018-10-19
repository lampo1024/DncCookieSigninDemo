using DncCookieSignin.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DncCookieSignin.Pages.Account
{
    public class RefreshModel : PageModel
    {
        private readonly SessionUserManagerService _userManagerService;
        public RefreshModel(SessionUserManagerService userManagerService)
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