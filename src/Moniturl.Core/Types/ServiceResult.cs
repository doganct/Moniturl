using Moniturl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moniturl.Core
{
    public class ServiceResult<T> : ServiceResult
    {
        public T Result { get; set; }
    }

    public class ServiceResult
    {
        public ServiceResult()
        {
            ErrorMessages = new Dictionary<string, string>();
        }
        public Dictionary<string, string> ErrorMessages { get; set; }
        public bool Success
        {
            get
            {
                return !ErrorMessages.Any();
            }
        }
    }
}
