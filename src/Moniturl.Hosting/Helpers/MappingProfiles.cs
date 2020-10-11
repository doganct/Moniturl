using AutoMapper;
using Moniturl.Core;

namespace Moniturl.Hosting
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, RegisterViewModel>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>().ReverseMap();
            CreateMap<TargetDto, TargetCreateViewModel>().ReverseMap();
            CreateMap<TargetDto, TargetUpdateViewModel>().ReverseMap();
        }
    }
}
