using Moniturl.Data;
using System;

namespace Moniturl.Core
{
    public class TargetToRequestSpecification : BaseSpecification<Target>
    {
        public TargetToRequestSpecification()
            : base(x => x.PassedMinutes >= x.Interval)
        {

        }
    }
}
