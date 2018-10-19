using DncCookieSignin.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace DncCookieSignin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMemoryCache _memoryCache;
        public LoginModel(IUserManagerService userManagerService
            ,IMemoryCache memoryCache)
        {
            _userManagerService = userManagerService;
            _memoryCache = memoryCache;
        }
        [BindProperty]
        public string ReturnUrl { get; set; }
        public string CacheCallbackMessage { get; set; }
        public void OnGet()
        {
            ReturnUrl = HttpContext.Request.Query["returnUrl"];
            var message = "";
            _memoryCache.Set("callbackMessage", "message from memory cache", System.TimeSpan.FromHours(1));
            _memoryCache.TryGetValue("callbackMessage",out message);
            
            CacheCallbackMessage = message;
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