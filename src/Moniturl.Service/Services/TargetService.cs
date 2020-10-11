using AutoMapper;
using Flurl.Http;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class TargetService : ITargetService
    {
        private readonly IGenericRepository<Target> _targerRepository;
        private readonly IMapper _mapper;

        public TargetService(IGenericRepository<Target> targerRepository, IMapper mapper)
        {
            this._targerRepository = targerRepository;
            this._mapper = mapper;
        }

        public async Task<ServiceResult<TargetDto>> AddAsync(TargetDto targetDto)
        {
            var target = _mapper.Map<Target>(targetDto);

            var addedTarget = await _targerRepository.AddAsync(target);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(addedTarget)
            };
        }

        public async Task<ServiceResult> CheckTargetResponses()
        {
            var targets = await GetTargetsToRequest();

            foreach (var target in targets)
            {
                var url = target.Url;
                var response = await url.GetAsync();

                //update target
                //if response another than 200, save mail table.
            }


            return new ServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(int targetId)
        {
            await _targerRepository.Delete(targetId);

            return new ServiceResult();
        }

        public async Task<ServiceResult<TargetDto>> GetTargetAsync(int id)
        {
            var target = await _targerRepository.GetByIdAsync(id);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(target)
            };
        }

        public async Task<ServiceResult<Pagination<TargetDto>>> GetTargetsAsync(TargetSearchParams targetSearchParams)
        {
            var spec = new TargetWithUserSpecification(targetSearchParams);

            var countSpec = new TargetWithFiltersForCountSpecification(targetSearchParams);

            var totalTargetCount = await _targerRepository.CountAsync(countSpec);

            var targets = await _targerRepository.GetAllBySpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TargetDto>>(targets);

            return new ServiceResult<Pagination<TargetDto>>
            {
                Result = new Pagination<TargetDto>(targetSearchParams.PageIndex, targetSearchParams.PageSize, totalTargetCount, data)
            };
        }

        public async Task<IReadOnlyList<TargetDto>> GetTargetsToRequest()
        {
            var spec = new TargetToRequestSpecification(DateTime.Now);

            var targets = await _targerRepository.GetAllBySpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<TargetDto>>(targets);

            return data;
        }

        public async Task<ServiceResult<TargetDto>> UpdateAsync(TargetDto targetDto)
        {
            var target = _mapper.Map<Target>(targetDto);

            var updatedTarget = await _targerRepository.UpdateAsync(target);

            return new ServiceResult<TargetDto>
            {
                Result = _mapper.Map<TargetDto>(updatedTarget)
            };
        }
    }
}
