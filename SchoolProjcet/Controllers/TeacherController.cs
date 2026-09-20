using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;
using SchoolProjcet.DTOs.TeacherDTOs;

namespace SchoolProjcet.Controllers
{
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
            var teachers = _context.Teachers.ToList();
            var DTOs = teachers.Select(t => new
            {
                t.Id,
                t.Name,
                t.Subject
            });
            return Ok(DTOs);
        }

        public IActionResult Get()
        {
     
                
            return Ok(item);

         
        }
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }
            return Ok(teacher);
        }
        [HttpPost]
        public IActionResult CreateTeacher([FromBody] TeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is null.");
            }
            var TeacherEntity = new Teacher
            {
                Name = teacher.Name,
                Subject = teacher.Subject
            };
            _context.Teachers.Add(TeacherEntity);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTeachers),
                new { id = TeacherEntity.Id },
                TeacherEntity);
        }
    }
}
