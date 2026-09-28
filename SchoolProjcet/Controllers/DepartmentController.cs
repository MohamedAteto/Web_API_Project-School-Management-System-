using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;
using SchoolProjcet.Mapping;
using SchoolProjcet.Reposatories.Implmentation;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]               
    public class DepartmentController :ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGenaricRepo<Department> genaricRepo;
        private readonly IMapper _mapper;

        public DepartmentController(AppDbContext context , IGenaricRepo<Department> genaricRepo)
        {
            _context = context;
            this.genaricRepo = genaricRepo;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<DepartmentProfile>())
                .CreateMapper();
        }


        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = genaricRepo.GetAll();

            if (departments is null || departments.Count == 0)
                return BadRequest("The Object is null");



            var DTOs = _mapper.Map<List<DepartmentDTO>>(departments);
           

            return Ok(DTOs);
        }


        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            var department = genaricRepo.GetById(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            var DTO = _mapper.Map<DepartmentDTO>(department);

            return Ok(department);
        }


        [HttpPost]
        public IActionResult CreateDepartment([FromBody] CreateDepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }

            var Entity = _mapper.Map<Department>(department);


            genaricRepo.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = Entity.Id }, Entity);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] UpdateDepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }
            var existingDepartment = genaricRepo.GetById(id);
            if (existingDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }


            _mapper.Map(department, existingDepartment);
            genaricRepo.Update(existingDepartment);
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
            genaricRepo.Delete(id);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
