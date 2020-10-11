using AutoMapper;
using Moniturl.Core;
using Moniturl.Data;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class TargetLogService : ITargetLogService
    {
        private readonly IGenericRepository<TargetLog> _targerLogRepository;
        private readonly IMapper _mapper;

        public TargetLogService(IGenericRepository<TargetLog> targerLogRepository, IMapper mapper)
        {
            this._targerLogRepository = targerLogRepository;
            this._mapper = mapper;
        }

        public async Task<ServiceResult<TargetLogDto>> AddAsync(TargetLogDto targetLogDto)
        {
            var targetLog = _mapper.Map<TargetLog>(targetLogDto);

            var addedTargetLog = await _targerLogRepository.AddAsync(targetLog);

            return new ServiceResult<TargetLogDto>
            {
                Result = _mapper.Map<TargetLogDto>(addedTargetLog)
            };
        }
    }
}
