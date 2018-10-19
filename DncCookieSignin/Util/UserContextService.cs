using DncCookieSignin.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DncCookieSignin.Util
{
    public class UserContextService
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static User GetUser
        {
            get
            {
                var user = new User
                {
                    Id = 1,
                    FirstName = "Rector",
                    LastName = "Liu",
                    Email = "example@email.com",
                    LoginName = "admin",
                    Password = "111111",
                    Roles = "admin"
                };
                return user;
            }
        }

        public static bool IsLogin
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                //if (!_httpContextAccessor.HttpContext.Session.SessionExists(SessionKeys.SessionKey(SessionKeys.UserLoginModel)))
                //{
                //    return false;
                //}
                //return true;
            }
        }

        public static User CurrentUser
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.Session.SessionExists(SessionKeys.SessionKey(SessionKeys.UserLoginModel)))
                {
                    return null;
                }
                return _httpContextAccessor.HttpContext.Session.Get<User>(SessionKeys.SessionKey(SessionKeys.UserLoginModel));
            }
        }

        public static string LoginName
        {
            get
            {
                //return CurrentUser?.LoginName;
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "loginName")?.Value;
            }
        }

        public static string DisplayName
        {
            get
            {
                var displayName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "displayName")?.Value;
                displayName = string.IsNullOrEmpty(displayName) ? "未知" : displayName;
                return displayName;
                //return CurrentUser?.FirstName + " " + CurrentUser?.LastName;
            }
        }

    }
}
