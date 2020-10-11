using AutoMapper;
using Moniturl.Core;
using Moniturl.Data;

namespace Moniturl.Service
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Target, TargetDto>();
        }
    }
}
