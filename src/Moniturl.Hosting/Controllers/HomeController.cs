using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moniturl.Core;
using Moniturl.Hosting;
using Moniturl.Hosting.Controllers;

namespace MonitUrl.Hosting.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITargetService _targetService;

        public HomeController(ITargetService targetService)
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
