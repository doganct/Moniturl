using Moniturl.Data;

namespace Moniturl.Core
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string email) : base(x => x.Email == email)
        {

        }

        public UserSpecification(string email, bool status) : base(x => x.Email == email && x.Status == status)
        {

        }
    }
}
