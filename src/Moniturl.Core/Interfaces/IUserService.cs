using Moniturl.Data;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface IUserService
    {
        public Task<ServiceResult> RegisterAsync(RegisterDto registerDto);
        public Task<ServiceResult<UserDto>> LoginAsync(LoginDto loginDto);
    }
}
