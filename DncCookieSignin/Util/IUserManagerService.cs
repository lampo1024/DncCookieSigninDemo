using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DncCookieSignin.Util
{
    public interface IUserManagerService
    {
        void SignIn(HttpContext httpContext, User user, bool isPersistent = false);
        void SignOut(HttpContext httpContext);
        void Refresh(HttpContext httpContext, User user);
    }
}
