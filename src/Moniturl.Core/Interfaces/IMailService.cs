using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface IMailService
    {
        Task<ServiceResult<MailDto>> AddAsync(MailDto mailDto);
        Task SendTargetIsDownMailAsync(TargetDto targetDto);
        Task SendMailAsync(string to, string subject, string body);

    }
}
