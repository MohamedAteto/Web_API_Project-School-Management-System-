using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
using SchoolProjcet.DTOs.SubjectDTOs;

namespace SchoolProjcet.Mapping
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile() {

            CreateMap<Subject, SubjectDTO>()
                .ForMember(dest => dest.TeacherName, op => op.MapFrom(s => s.Teacher.FirstName + " " + s.Teacher.LastName)).ReverseMap();
            CreateMap<Subject, CreateSubjectDTO>().ReverseMap();
            CreateMap<Subject,UpdateSubjectDTO>().ReverseMap();
        }
    }
}
