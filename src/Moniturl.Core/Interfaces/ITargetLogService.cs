using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface ITargetLogService
    {
        Task<ServiceResult<TargetLogDto>> AddAsync(TargetLogDto targetDto);
    }
}
