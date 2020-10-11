using Hangfire.Dashboard;
using Moniturl.Core;
using System;
using System.Linq;
using System.Security.Claims;

namespace Moniturl.Hosting
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (httpContext.User == null ||
                httpContext.User.Claims == null ||
                httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role) == null)
            {
                return false;
            }

            var userRole = (Role)Enum.Parse(typeof(Role), httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value);

            return userRole == Role.Admin;
        }
    }
}
