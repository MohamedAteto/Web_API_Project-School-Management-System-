using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;
using SchoolProjcet.Mapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]               
    public class DepartmentController :ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentController()
        {
            _context = new AppDbContext();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<DepartmentProfile>())
                .CreateMapper();
        }


        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _context.Departments.ToList();

            if (departments is null || departments.Count == 0)
                return BadRequest("The Object is null");

            ///  old Method ////

            //var DTOs = departments.Select(d => new
            //{
            //    d.Id,
            //    d.Name,
            //    d.Description
            //});


            var DTOs = _mapper.Map<List<DepartmentDTO>>(departments);
           

            return Ok(DTOs);
        }


        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            var DTO = _mapper.Map<DepartmentDTO>(department);

            return Ok(department);
        }


        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }

            ///  old Method ////

            //var DepartmentEntity = new Department
            //{
            //    Name = department.Name,
            //    Description = department.Description
            //};

            var Entity = _mapper.Map<Department>(department);


            _context.Departments.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, Entity);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] UpdateDepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }
            var existingDepartment = _context.Departments.Find(id);
            if (existingDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            ///  old Method ////

            //existingDepartment.Name = department.Name;
            //existingDepartment.Description = department.Description;
            _mapper.Map(department, existingDepartment);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment( [FromRoute] int id)
        {
            var existingDepartment = _context.Departments.Find(id);
            if (existingDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            _context.Departments.Remove(existingDepartment);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
