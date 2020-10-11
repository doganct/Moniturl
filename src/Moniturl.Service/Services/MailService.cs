using AutoMapper;
using Moniturl.Core;
using Moniturl.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moniturl.Service
{
    public class MailService : IMailService
    {
        private readonly IGenericRepository<Mail> _mailRepository;
        private readonly IMapper _mapper;

        public MailService(IGenericRepository<Mail> mailRepository, IMapper mapper)
        {
            this._mailRepository = mailRepository;
            this._mapper = mapper;
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
    }
}
