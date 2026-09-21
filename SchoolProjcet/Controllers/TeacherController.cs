using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.TeacherDTOs;
using SchoolProjcet.Mapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TeacherController()
        {
            _context = new AppDbContext();
            _mapper = new MapperConfiguration(conf => conf.AddProfile<TeacherProfile>())
                .CreateMapper();

        }

        // GET: api/Teacher
        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = _context.Teachers
                .Include(t => t.Department).ToList();


            var DTO = _mapper.Map<List<TeacherDTO>>(teachers);
            
            return Ok(DTO);
        }


        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.Department)
                .FirstOrDefault(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            //var dto = new TeacherDTO
            //{
            //    FullName = teacher.FirstName + " " + teacher.LastName,
            //    DepartmentId = teacher.DepartmentId,
            //    DepartmentName = teacher.Department != null
            //        ? teacher.Department.Name
            //        : null,
            //    Email = teacher.Email,
            //    PhoneNumber = teacher.PhoneNumber,
            //    Salary = teacher.Salary
            //};

            var dto = _mapper.Map<TeacherDTO>(teacher);

            return Ok(dto);
        }


        // POST: api/Teacher
        [HttpPost]
        public IActionResult CreateTeacher([FromBody] CreateTeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is null.");
            }

            //var teacherEntity = new Teacher
            //{
            //    FirstName = teacher.FirstName,
            //    LastName = teacher.LastName,
            //    Email = teacher.Email,
            //    PhoneNumber = teacher.PhoneNumber,
            //    DepartmentId = teacher.DepartmentId,
            //    Salary = teacher.Salary
            //};

            var Entity = _mapper.Map<Teacher>(teacher);

            _context.Teachers.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeacherById), new { id = Entity.Id }, teacher);
        }


        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public IActionResult UpdateTeacher([FromRoute] int id,  [FromBody] UpdateTeacherDTO updatedTeacher)
        {
            if (updatedTeacher == null)
            {
                return BadRequest("Teacher data is null.");
            }

            var teacher = _context.Teachers.Find(id);

            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }

            //teacher.FirstName = updatedTeacher.FirstName;
            //teacher.LastName = updatedTeacher.LastName;
            //teacher.Email = updatedTeacher.Email;
            //teacher.PhoneNumber = updatedTeacher.PhoneNumber;
            //teacher.DepartmentId = updatedTeacher.DepartmentID;
            //teacher.Salary = updatedTeacher.Salary;

            _mapper.Map(destination: teacher, source: updatedTeacher);

            _context.SaveChanges();

            return NoContent();
        }
    }
}