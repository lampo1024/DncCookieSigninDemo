using DncCookieSignin.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DncCookieSignin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManagerService _userManagerService;
        public LoginModel(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        [BindProperty]
        public string ReturnUrl { get; set; }
        public void OnGet()
        {
            ReturnUrl = HttpContext.Request.Query["returnUrl"];
        }

        public IActionResult OnPost()
        {
            _userManagerService.SignIn(HttpContext, UserContextService.GetUser);
            TempData["message"] = "登录成功";
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToPage("/account/profile");
        }
    }
}