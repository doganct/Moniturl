using System;
using System.Collections.Generic;
using System.Text;

namespace Moniturl.Core
{
    public class TargetDto : BaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public int? UserId { get; set; }
        public DateTime LastRequestTime { get; set; }
    }
}
