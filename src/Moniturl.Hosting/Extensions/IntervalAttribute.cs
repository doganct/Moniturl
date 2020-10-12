using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Moniturl.Hosting
{
    public class IntervalAttribute : ValidationAttribute
    {
        public IntervalAttribute() { }

        public override bool IsValid(object value)
        {
            var number = value as int?;

            if (!number.HasValue)
                return false;

            return number > 0;
        }
    }
}
