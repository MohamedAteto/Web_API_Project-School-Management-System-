using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]               
    public class DepartmentController :ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController()
        {
            _context = new AppDbContext();
        }


        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _context.Departments.ToList();

            var DTOs = departments.Select(d => new
            {
                d.Id,
                d.Name,
                d.Description
            });

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
            return Ok(department);
        }


        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }

            var DepartmentEntity = new Department
            {
                Name = department.Name,
                Description = department.Description
            };

            _context.Departments.Add(DepartmentEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = DepartmentEntity.Id }, DepartmentEntity);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDTO department)
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
            existingDepartment.Name = department.Name;
            existingDepartment.Description = department.Description;
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
