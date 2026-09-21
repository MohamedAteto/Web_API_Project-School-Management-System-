using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;

namespace SchoolProjcet.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile() {

            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.FullName, op => op.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(dest => dest.CalssRoomName ,op => op.MapFrom(c => c.ClassRoom.Name)).ReverseMap();

            CreateMap<Student,CreateStudentDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDTO>().ReverseMap();
        }
    }
}
