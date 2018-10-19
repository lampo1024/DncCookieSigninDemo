using DncCookieSignin.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace DncCookieSignin.Util
{
    public class UserManagerService
    {
        //private readonly User _user;
        //public UserManagerService()
        //{
        //    _user = new User
        //    {
        //        Id = 1,
        //        FirstName = "Rector",
        //        LastName = "Liu",
        //        Email = "example@email.com",
        //        LoginName = "admin",
        //        Password = "111111",
        //        Roles = "admin"
        //    };
        //}
        public void SignIn(HttpContext httpContext, User user, bool isPersistent = false)
        {
            //ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            //ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            //await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            httpContext.Session.Set(SessionKeys.SessionKey(SessionKeys.UserLoginModel), user);
        }

        public void SignOut(HttpContext httpContext)
        {
            //httpContext.SignOutAsync();
            httpContext.Session.Remove(SessionKeys.SessionKey(SessionKeys.UserLoginModel));
        }

        public void Refresh(HttpContext httpContext, User user)
        {
            //ClaimsIdentity identity = new ClaimsIdentity(this.GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            //ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            //await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            SignIn(httpContext,user);
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("loginName", user.Email));
            claims.Add(new Claim("displayName", user.FirstName + user.LastName));
            claims.AddRange(this.GetUserRoleClaims(user));
            return claims;
        }

        private IEnumerable<Claim> GetUserRoleClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Roles));
            return claims;
        }
    }
}
