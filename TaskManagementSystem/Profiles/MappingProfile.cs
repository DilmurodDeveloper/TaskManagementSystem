using AutoMapper;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Entities;

namespace TaskManagementSystem.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project
            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.UserName,
                           o => o.MapFrom(s => s.User.Username));
            CreateMap<CreateProjectDto, Project>();

            // Task
            CreateMap<TaskEntity, TaskDto>()
                .ForMember(d => d.ProjectName,
                           o => o.MapFrom(s => s.Project.Name));
            CreateMap<CreateTaskDto, TaskEntity>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom(src => src.Password));
        }
    }
}
