using System;
using System.Collections.Generic;
using System.Text;

namespace Moniturl.Data
{
    public class TargetLog : BaseEntity
    {
        public string StatusCode { get; set; }
        public int? TargetId { get; set; }

        public virtual Target Target { get; set; }
    }
}
