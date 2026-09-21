using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;

namespace SchoolProjcet.Mapping
{
    public class DepartmentProfile : Profile 
    {
        public DepartmentProfile() {


            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Department,CreateTepertmentDTO>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();
            


        }
    }
}
