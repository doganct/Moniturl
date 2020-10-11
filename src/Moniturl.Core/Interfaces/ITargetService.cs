using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface ITargetService
    {
        public Task<ServiceResult<Pagination<TargetDto>>> GetTargetsAsync(TargetSearchParams targetSearchParams);
        public Task<ServiceResult<TargetDto>> GetTargetAsync(int id);
        public Task<ServiceResult<TargetDto>> AddAsync(TargetDto targetDto);
        public Task<ServiceResult<TargetDto>> UpdateAsync(TargetDto targetDto);
        public Task<ServiceResult> DeleteAsync(int targetId);
        public Task<IReadOnlyList<TargetDto>> GetTargetsToRequest();
        public Task<ServiceResult> CheckTargetResponses();
    }
}
