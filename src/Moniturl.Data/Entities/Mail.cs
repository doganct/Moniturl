using System;
using System.Collections.Generic;
using System.Text;

namespace Moniturl.Data
{
    public class Mail : BaseEntity
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsSend { get; set; }
    }
}
