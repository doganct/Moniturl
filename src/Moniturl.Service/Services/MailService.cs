using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class MailService : IMailService
    {
        private readonly IGenericRepository<Mail> _mailRepository;
        private readonly IMapper _mapper;
        private readonly SmtpSettings _smtpSettings;

        public MailService(
            IGenericRepository<Mail> mailRepository, 
            IMapper mapper,
            IOptions<SmtpSettings> smtpSettings)
        {
            this._mailRepository = mailRepository;
            this._mapper = mapper;
            this._smtpSettings = smtpSettings.Value;
        }
        public async Task<ServiceResult<MailDto>> AddAsync(MailDto mailDto)
        {
            var mail = _mapper.Map<Mail>(mailDto);

            var addedMail = await _mailRepository.AddAsync(mail);

            return new ServiceResult<MailDto>
            {
                Result = _mapper.Map<MailDto>(addedMail)
            };
        }

        public async Task SendMailAsync(string to, string subject, string body)
        {
            var mailDto = new MailDto
            {
                Text = body,
                Title = subject,
                To = to
            };

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Username));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.Ssl);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                mailDto.IsSend = true;
            }
            catch(Exception ex)
            {
                //TODO: log error
                mailDto.IsSend = false;
            }
            finally
            {
                await AddAsync(mailDto);
            }
           
        }
    }
}
