using Moniturl.Data;

namespace Moniturl.Core
{
    public class TargetLogSpecification : BaseSpecification<TargetLog>
    {
        public TargetLogSpecification(TargetLogSearchParams targetLogSearchParams) 
            : base(x => x.TargetId == targetLogSearchParams.TargetId && x.Status)
        {
            AddOrderByDescending(x => x.CreatedDate);

            ApplyPaging(targetLogSearchParams.Skip, targetLogSearchParams.Take);

            if (!string.IsNullOrEmpty(targetLogSearchParams.Sort))
            {
                switch (targetLogSearchParams.Sort)
                {
                    case "createdDateAsc":
                        AddOrderBy(p => p.CreatedDate);
                        break;
                    default:
                        AddOrderByDescending(p => p.CreatedDate);
                        break;

                }
            }
        }
    }
}
