using Moniturl.Data;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterAsync(RegisterDto registerDto);
        Task<ServiceResult<UserDto>> LoginAsync(LoginDto loginDto);
    }
}
