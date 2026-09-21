using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.TeacherDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            var item = _context.Teachers
                .Include(t => t.Department)
                .Select(t => new TeacherDTO
                {
                    FullName = t.FirstName + " " + t.LastName,
                    DepartmentId = t.DepartmentId,
                    DepartmentName = t.Department != null ? t.Department.Name : null,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Salary = t.Salary,
                })
                .ToList();

            return Ok(item);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            // IMPORTANT: use Include + FirstOrDefault instead of Find,
            // because Find does NOT load the Department navigation property.
            var teacher = _context.Teachers
                .Include(t => t.Department)
                .FirstOrDefault(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            var DTO = new TeacherDTO
            {
                FullName = teacher.FirstName + " " + teacher.LastName,
                DepartmentId = teacher.DepartmentId,
                DepartmentName = teacher.Department != null ? teacher.Department.Name : null,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Salary = teacher.Salary
            };

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] CreateTeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is null.");
            }

            var TeacherEntity = new Teacher
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Salary = teacher.Salary,
                DepartmentId = teacher.DepartmentId,
            };

            _context.Teachers.Add(TeacherEntity);
            _context.SaveChanges();

            // Build a proper TeacherDTO (not the input DTO) so the response shape
            // matches GET endpoints.
            var dto = new TeacherDTO
            {
                FullName = TeacherEntity.FirstName + " " + TeacherEntity.LastName,
                DepartmentId = TeacherEntity.DepartmentId,
                DepartmentName = _context.Departments.Where(d => d.Id == TeacherEntity.DepartmentId).Select(d => d.Name) .FirstOrDefault(),
                Email = TeacherEntity.Email,
                PhoneNumber = TeacherEntity.PhoneNumber,
                Salary = TeacherEntity.Salary
            };

            return CreatedAtAction(nameof(GetTeacherById), new { id = TeacherEntity.Id }, dto);
        }
    }
}