using System;
using System.Collections.Generic;
using System.Text;

namespace Moniturl.Core
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Role RoleId { get; set; }
    }
}
