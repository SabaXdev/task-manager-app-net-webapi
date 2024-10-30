using AutoMapper;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTaskManagerRequest, MyTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateTaskManagerRequest, MyTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
