using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moniturl.Hosting.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
