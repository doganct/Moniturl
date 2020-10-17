using Moniturl.Data;

namespace Moniturl.Core
{
    public class TargetLogForCountSpecification : BaseSpecification<TargetLog>
    {
        public TargetLogForCountSpecification(TargetLogSearchParams targetSearchParams)
          : base(x => x.TargetId == targetSearchParams.TargetId
          && x.Status
          )
        {

        }
    }
}
