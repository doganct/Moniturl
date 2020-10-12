using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface ITargetService
    {
        Task<ServiceResult<Pagination<TargetDto>>> GetTargetsAsync(TargetSearchParams targetSearchParams);
        Task<ServiceResult<TargetDto>> GetTargetAsync(int id);
        Task<ServiceResult<TargetDto>> AddAsync(TargetDto targetDto);
        Task<ServiceResult<TargetDto>> UpdateAsync(TargetDto targetDto);
        Task<ServiceResult> DeleteAsync(int targetId);
        Task<IReadOnlyList<TargetDto>> GetTargetsToRequest();
        Task<ServiceResult> CheckTargetResponses(string emailAddress);
    }
}
