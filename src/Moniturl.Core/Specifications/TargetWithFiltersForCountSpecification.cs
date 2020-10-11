using Moniturl.Data;
using System;

namespace Moniturl.Core
{
    public class TargetWithFiltersForCountSpecification : BaseSpecification<Target>
    {
        public TargetWithFiltersForCountSpecification(TargetSearchParams targetSearchParams)
            : base(x =>
                (string.IsNullOrEmpty(targetSearchParams.Search) || x.Name.ToLower().Contains(targetSearchParams.Search)) &&
                (!targetSearchParams.UserId.HasValue || x.UserId == targetSearchParams.UserId)
            )
        {
           
        }
    }
}