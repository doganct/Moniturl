using AutoMapper;
using Moniturl.Core;
using Moniturl.Data;
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

        public async Task<ServiceResult<Pagination<TargetDto>>> GetTargetsAsync(TargetSearchParams targetSearchParams)
        {
            var spec = new TargetWithUserSpecification(targetSearchParams);

            var countSpec = new TargetWithFiltersForCountSpecification(targetSearchParams);

            var totalTargetCount = await _targerRepository.CountAsync(countSpec);

            var targets = await _targerRepository.GetAllBySpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Target>, IReadOnlyList<TargetDto>>(targets);

            return new ServiceResult<Pagination<TargetDto>>
            {
                Result = new Pagination<TargetDto>(targetSearchParams.PageIndex, targetSearchParams.PageSize, totalTargetCount, data)
            };
        }
    }
}
