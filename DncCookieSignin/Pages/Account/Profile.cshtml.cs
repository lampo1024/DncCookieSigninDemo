using DncCookieSignin.Extensions;
using DncCookieSignin.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace DncCookieSignin.Pages.Account
{
    //[SessionAuthorize]
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public ProfileModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public string AspNetCoreCookie { get; set; }
        public string CacheCallbackMessage { get; set; }
        public void OnGet()
        {
            var session = HttpContext.Session.Get<User>(SessionKeys.SessionKey(SessionKeys.UserLoginModel));
            AspNetCoreCookie = Request.Cookies[".AspNetCore.Cookies"];
            CacheCallbackMessage = _memoryCache.Get<string>("callbackMessage");
        }
    }
}