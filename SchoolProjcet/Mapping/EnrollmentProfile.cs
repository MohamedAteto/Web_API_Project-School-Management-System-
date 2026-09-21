using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.EnrollmentDTOs;

namespace SchoolProjcet.Mapping
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile() {

            CreateMap<Enrollment, EnrollmentDTO>()
                .ForMember(dest => dest.StudentName, op => op.MapFrom(s => s.Student.FirstName + " " + s.Student.LastName))
                .ForMember(dest => dest.SubjectName, op => op.MapFrom(s => s.Subject.Name)).ReverseMap();

            CreateMap<Enrollment, CreateEnrollmentDTO>().ReverseMap();
            CreateMap<Enrollment, UpdateEnrollmentDTO>().ReverseMap();

        }
    }
}
