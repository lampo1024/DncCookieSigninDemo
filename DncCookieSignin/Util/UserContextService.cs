using DncCookieSignin.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncCookieSignin.Util
{
    public class UserContextService
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static HttpContext httpContext;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            httpContext = _httpContextAccessor.HttpContext;
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
                //return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                if (!_httpContextAccessor.HttpContext.Session.SessionExists(SessionKeys.SessionKey(SessionKeys.UserLoginModel))){
                    return false;
                }
                return true;
            }
        }

        public static string LoginName { get
            {
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
            }
        }

    }
}
