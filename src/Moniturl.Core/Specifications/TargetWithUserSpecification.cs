using Moniturl.Data;
using System;

namespace Moniturl.Core
{
    public class TargetWithUserSpecification : BaseSpecification<Target>
    {
        public TargetWithUserSpecification(TargetSearchParams targetSearchParams)
            : base(x =>
                (string.IsNullOrEmpty(targetSearchParams.Search) || x.Name.ToLower().Contains(targetSearchParams.Search)) &&
                (!targetSearchParams.UserId.HasValue || x.UserId == targetSearchParams.UserId)
            && x.Status
            )
        {
            AddInclude(x => x.User);

            AddOrderBy(x => x.Name);

            ApplyPaging(targetSearchParams.PageSize * (targetSearchParams.PageIndex - 1), targetSearchParams.PageSize);

            if (!string.IsNullOrEmpty(targetSearchParams.Sort))
            {
                switch (targetSearchParams.Sort)
                {
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
        }

        public TargetWithUserSpecification(int id) : base(x => x.Id == id && x.Status)
        {
            AddInclude(x => x.User);
        }
    }
}