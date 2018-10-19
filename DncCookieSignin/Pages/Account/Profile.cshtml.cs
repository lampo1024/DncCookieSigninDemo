using DncCookieSignin.Extensions;
using DncCookieSignin.Util;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DncCookieSignin.Pages.Account
{
    [SessionAuthorize]
    public class ProfileModel : PageModel
    {
        public string AspNetCoreCookie { get; set; }
        public void OnGet()
        {
            var session = HttpContext.Session.Get<User>(SessionKeys.SessionKey(SessionKeys.UserLoginModel));
            AspNetCoreCookie = Request.Cookies[".AspNetCore.Cookies"];
        }
    }
}