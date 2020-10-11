using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moniturl.Core;

namespace Moniturl.Hosting
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public Role Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var isAuthorized = true;

            if (filterContext.HttpContext.User == null ||
            filterContext.HttpContext.User.Claims == null ||
            filterContext.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role) == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary { { "controller", "Auth" }, { "action", "Logout" } });
                return;
            }

            var userRole = (Role)Enum.Parse(typeof(Role), filterContext.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value);

            if (Role != 0 && ((Role & userRole) != userRole))
            {
                isAuthorized = false;
            }


            if (!isAuthorized)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Auth" }, { "action", "Logout" } });
                return;
            }

        }

    }
}