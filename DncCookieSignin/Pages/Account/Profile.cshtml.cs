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
            AspNetCoreCookie = Request.Cookies[".AspNetCore.Cookies"];
        }
    }
}