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
        public async Task<IActionResult> GetTargets(JqueryDatatableQueryModel model)
        {
            var data = await _targetService.GetTargetsAsync(new TargetSearchParams
            {
                Search = model.Search.Value ?? "",
                Take = Convert.ToInt32(model.Length),
                Skip = Convert.ToInt32(model.Start),
                UserId = UserId
            });

            return Json(new
            {
                draw = model.Draw,
                recordsFiltered = data.Result.Count,
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
                AddModelErrors(serviceResult);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var isAuthResult = await _targetService.CheckAuthorization(id, UserId);
            if (!isAuthResult.Success)
            {
                AddModelErrors(isAuthResult);
                return View(nameof(Index));
            }


            var serviceResult = await _targetService.GetTargetAsync(id);

            if (!serviceResult.Success)
            {
                AddModelErrors(serviceResult);
                return View(nameof(Index));
            }

            var targetUpdateViewModel = _mapper.Map<TargetUpdateViewModel>(serviceResult.Result);

            return View(targetUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TargetUpdateViewModel model)
        {
            var isAuthResult = await _targetService.CheckAuthorization(model.Id, UserId);
            if (!isAuthResult.Success)
            {
                AddModelErrors(isAuthResult);
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var getTargetServiceResult = await _targetService.GetTargetAsync(model.Id);
            var targetDto = getTargetServiceResult.Result;

            _mapper.Map<TargetUpdateViewModel, TargetDto>(model, targetDto);

            targetDto.UserId = UserId;
            var serviceResult = await _targetService.UpdateAsync(targetDto);

            if (!serviceResult.Success)
            {
                AddModelErrors(serviceResult);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isAuthResult = await _targetService.CheckAuthorization(id, UserId);
            if (!isAuthResult.Success)
            {
                AddModelErrors(isAuthResult);
                return View(nameof(Index));
            }


            var serviceResult = await _targetService.DeleteAsync(id);

            if (!serviceResult.Success)
            {
                AddModelErrors(serviceResult);
                return View(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CheckInterval(int interval)
        {
            return interval > 1;
        }

        private void AddModelErrors(ServiceResult serviceResult)
        {
            foreach (var error in serviceResult.ErrorMessages)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }

    }
}
