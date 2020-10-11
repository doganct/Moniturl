using Moniturl.Data;
using System;

namespace Moniturl.Core
{
    public class TargetToRequestSpecification : BaseSpecification<Target>
    {
        public TargetToRequestSpecification(DateTime dateTime)
            : base(x =>
            x.LastRequestTime.HasValue ?
            dateTime > x.LastRequestTime.Value :
            true)
        {

        }
    }
}
