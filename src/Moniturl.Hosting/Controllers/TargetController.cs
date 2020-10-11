using Microsoft.AspNetCore.Mvc;
using Moniturl.Core;
using System.Threading.Tasks;

namespace Moniturl.Hosting.Controllers
{
    public class TargetController : Controller
    {
        private readonly ITargetService _targetService;

        public TargetController(ITargetService targetService)
        {
            this._targetService = targetService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(TargetSearchParams targetSearchParams)
        {
            var data = await _targetService.GetTargetsAsync(targetSearchParams);

            return View(data.Result);
        }
    }
}
