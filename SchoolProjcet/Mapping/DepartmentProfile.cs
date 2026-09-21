using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;

namespace SchoolProjcet.Mapping
{
    public class DepartmentProfile : Profile 
    {
        public DepartmentProfile() {


            CreateMap<Department, DepartmentDTO>().ReverseMap();
            


        }
    }
}
