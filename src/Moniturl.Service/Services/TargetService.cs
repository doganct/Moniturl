using AutoMapper;
using Hangfire;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class TargetService : ITargetService
    {
        private readonly IGenericRepository<Target> _targetRepository;
        private readonly ITargetLogService _targetLogService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public TargetService(
            IGenericRepository<Target> targetRepository,
            ITargetLogService targetLogService,
            IMailService mailService,
            IMapper mapper)
        {
            this._targetRepository = targetRepository;
            this._targetLogService = targetLogService;
            this._mailService = mailService;
            this._mapper = mapper;
        }

        public async Task<ServiceResult<TargetDto>> AddAsync(TargetDto targetDto)
        {
            targetDto.LastRequestTime = DateTime.Now;

            var target = _mapper.Map<Target>(targetDto);

            var addedTarget = await _targetRepository.AddAsync(target);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(addedTarget)
            };
        }

        public async Task<ServiceResult> CheckAuthorization(int targetId, int userId)
        {
            var result = new ServiceResult();

            var target = await _targetRepository.GetByIdAsync(targetId);
            if(target.UserId != userId)
            {
                result.ErrorMessages.Add(string.Empty, Messages.Forbidden);
            }

            return result;
        }

        public async Task<ServiceResult> CheckTargetResponses()
        {
            var targets = await GetTargetsToRequest();

            var httpClient = new HttpClient();

            foreach (var target in targets)
            {
                var url = target.Url;
                var response = await httpClient.GetAsync(url);

                //save target log
                await _targetLogService.AddAsync(new TargetLogDto
                {
                    StatusCode = ((int)response.StatusCode).ToString(),
                    TargetId = target.Id
                });

                //update target
                target.LastRequestTime = DateTime.Now;
                await UpdateAsync(target);

                //if response another than 200, send mail or notification...
                if (!response.IsSuccessStatusCode)
                {
                    BackgroundJob.Enqueue(() => _mailService.SendTargetIsDownMailAsync(target));
                }
            }

            return new ServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(int targetId)
        {
            await _targetRepository.Delete(targetId);

            return new ServiceResult();
        }

        public async Task<ServiceResult<TargetDto>> GetTargetAsync(int id)
        {
            var target = await _targetRepository.GetByIdAsync(id);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(target)
            };
        }

        public async Task<ServiceResult<Pagination<TargetDto>>> GetTargetsAsync(TargetSearchParams targetSearchParams)
        {
            var spec = new TargetWithUserSpecification(targetSearchParams);

            var countSpec = new TargetWithFiltersForCountSpecification(targetSearchParams);

            var totalTargetCount = await _targetRepository.CountAsync(countSpec);

            var targets = await _targetRepository.GetAllBySpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TargetDto>>(targets);

            return new ServiceResult<Pagination<TargetDto>>
            {
                Result = new Pagination<TargetDto>(targetSearchParams.Skip, targetSearchParams.Take, totalTargetCount, data)
            };
        }

        public async Task<IReadOnlyList<TargetDto>> GetTargetsToRequest()
        {
            var spec = new TargetToRequestSpecification();

            var targets = await _targetRepository.GetAllBySpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TargetDto>>(targets);

            return data;
        }

        public async Task<ServiceResult<TargetDto>> UpdateAsync(TargetDto targetDto)
        {
            var target = _mapper.Map<Target>(targetDto);

            var updatedTarget = await _targetRepository.UpdateAsync(target);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(updatedTarget)
            };
        }
    }
}
