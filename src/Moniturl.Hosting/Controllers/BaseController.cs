using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Moniturl.Hosting.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public int UserId
        {
            get
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Convert.ToInt32(id);
            }
        }

        public string UserEmail
        {
            get
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                return email;
            }
        }
    }
}
