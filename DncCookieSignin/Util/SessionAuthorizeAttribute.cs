using DncCookieSignin.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DncCookieSignin.Util
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SessionAuthorizeAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.HttpContext.Session.SessionExists(SessionKeys.SessionKey(SessionKeys.UserLoginModel)))
            {
                var result = new RedirectToPageResult("/account/login");
                var request = context.HttpContext.Request;
                var url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString.Value;
                var query = context.HttpContext.Request.Query;
                var dict = new Microsoft.AspNetCore.Routing.RouteValueDictionary(new { returnUrl = url });
                //foreach (var item in query.Keys)
                //{
                //    dict.Add(item, query[item]);
                //}

                result.RouteValues = dict;
                context.Result = result;
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
