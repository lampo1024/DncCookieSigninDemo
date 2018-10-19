using DncCookieSignin.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace DncCookieSignin.Util
{
    public class SessionUserManagerService : IUserManagerService
    {
        public void SignIn(HttpContext httpContext, User user, bool isPersistent = false)
        {
            httpContext.Session.Set(SessionKeys.SessionKey(SessionKeys.UserLoginModel), user);
        }

        public void SignOut(HttpContext httpContext)
        {
            httpContext.Session.Remove(SessionKeys.SessionKey(SessionKeys.UserLoginModel));
            httpContext.Session.Clear();
        }

        public void Refresh(HttpContext httpContext, User user)
        {
            SignIn(httpContext, user);
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
