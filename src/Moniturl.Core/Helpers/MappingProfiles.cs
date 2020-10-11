using AutoMapper;
using Moniturl.Data;

namespace Moniturl.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Target, TargetDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>();
        }
    }
}
