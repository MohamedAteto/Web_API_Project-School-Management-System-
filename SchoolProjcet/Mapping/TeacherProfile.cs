using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.TeacherDTOs;

namespace SchoolProjcet.Mapping
{
    public class TeacherProfile : Profile
    {

        public TeacherProfile() 
        {

            CreateMap<Teacher, TeacherDTO>()
                .ForMember(dest => dest.FullName, op => op.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.DepartmentName, op => op.MapFrom(n => n.Department.Name));

            CreateMap<Teacher, UpdateTeacherDTO>().ReverseMap();
            CreateMap<Teacher, CreateTeacherDTO>().ReverseMap();














               

        }
    }
}
