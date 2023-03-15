using AutoMapper;
using TodoList.BLL.Models;
using TodoList.DAL.Entities;

namespace TodoList.BLL.MappingProfiles
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<AddOrUpdateTaskVM, Todo>();
            // .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        }
    }
}