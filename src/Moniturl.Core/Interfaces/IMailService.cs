using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface IMailService
    {
        public Task<ServiceResult<MailDto>> AddAsync(MailDto targetDto);

    }
}
