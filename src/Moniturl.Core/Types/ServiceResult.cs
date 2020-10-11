using Moniturl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moniturl.Core
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            ErrorMessages = new Dictionary<string, string>();
        }

        public T Result { get; set; }
        public Dictionary<string,string> ErrorMessages { get; set; }
        public bool Success
        {
            get
            {
                return !ErrorMessages.Any();
            }
        }
    }
}
