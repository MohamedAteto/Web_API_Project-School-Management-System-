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
            var Teachers = _context.Teachers.Include(d => d.Department).Select(x => new TeacherDTO
            {
                FullName = x.FirstName + " " + x.LastName,
                Email  = x.Email,
                DepartmentName = x.Department.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();
            return Ok(Teachers);
        }

   
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            var DTO = new TeacherDTO()
            {
                FullName = teacher.FirstName + " " + teacher.LastName,
                Email = teacher.Email,
                DepartmentName = teacher.Department.Name,
                PhoneNumber = teacher.PhoneNumber
            };
            return Ok(DTO);
        }
        [HttpPost]
        public IActionResult CreateTeacher([FromBody] CreateTeacherDTO teacher)
        {
            if (teacher is null)
                return BadRequest("The Object Is null B !");

            var normaleEntity = new Teacher()
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                DepartmentId = teacher.DepartmentId,
                Salary = teacher.Salary
            };
            
            _context.Teachers.Add(normaleEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeacherById), new { Id = normaleEntity.Id }, normaleEntity);
            //return CreatedAtAction(nameof(GetTeacherById), normaleEntity.Id , normaleEntity);

        }

        [HttpPut ("{Id}")]
        public IActionResult UpdateTeacher([FromRoute] int id ,[FromBody] UpdateTeacherDTO updatedTeacher)
        {
            if (updatedTeacher is null)
                return BadRequest("The object not found or isn't here ");


            var item = _context.Teachers.Find(id);

            if (item is null)
                return BadRequest("The Object is null Baby");

            item.FirstName = updatedTeacher.FirstName;
            item.LastName = updatedTeacher.LastName;
            item.Email = updatedTeacher.Email;
            item.PhoneNumber = updatedTeacher.PhoneNumber;
            item.DepartmentId = updatedTeacher.DepartmentId;
            item.Salary = updatedTeacher.Salary;

            _context.SaveChanges();
            return NoContent();

        }
    }
}
