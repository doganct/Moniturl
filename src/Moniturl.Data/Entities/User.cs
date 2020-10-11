using System.Collections.Generic;

namespace Moniturl.Data
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public virtual ICollection<Target> Targets { get; set; }
    }
}
