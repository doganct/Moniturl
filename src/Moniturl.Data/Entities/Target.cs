using System;
using System.Collections.Generic;

namespace Moniturl.Data
{
    public class Target : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public int? UserId { get; set; }
        public DateTime? LastRequestTime { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<TargetLog> TargetLogs { get; set; }
    }
}
