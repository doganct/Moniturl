using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public HomeController(ITargetService targetService, IMapper mapper)
        {
            this._targetService = targetService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTargets()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var skip = Request.Form["start"].FirstOrDefault();
            var take = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            var data = await _targetService.GetTargetsAsync(new TargetSearchParams
            {
                Search = searchValue,
                PageSize = Convert.ToInt32(take),
                PageIndex = 1,
                UserId = UserId
            });

            return Json(new
            {
                draw = draw,
                recordsFiltered = data.Result.Data.Count,
                recordsTotal = data.Result.Count,
                data = data.Result.Data
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TargetCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var targetDto = _mapper.Map<TargetDto>(model);
            targetDto.UserId = UserId;
            var serviceResult = await _targetService.AddAsync(targetDto);

            if (!serviceResult.Success)
            {
                foreach (var error in serviceResult.ErrorMessages)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
