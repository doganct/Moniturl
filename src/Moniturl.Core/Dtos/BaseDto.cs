using System;

namespace Moniturl.Core
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
