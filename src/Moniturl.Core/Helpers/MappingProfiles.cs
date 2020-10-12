using AutoMapper;
using Moniturl.Data;

namespace Moniturl.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TargetDto, Target>();
            CreateMap<Target, TargetDto>()
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>();
            CreateMap<TargetLog, TargetLogDto>().ReverseMap();
            CreateMap<MailDto, Mail>().ReverseMap();
        }
    }
}
