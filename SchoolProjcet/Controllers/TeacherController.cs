using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.TeacherDTOs;
using SchoolProjcet.Mapping;
using SchoolProjcet.Reposatories.Implmentation;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGenaricRepo<Teacher> teacherRepo;
        private readonly IMapper _mapper;
        public TeacherController(IGenaricRepo<Teacher> teacherRepo,AppDbContext context)
        {
            _context = context; 
            this.teacherRepo = teacherRepo;
            _mapper = new MapperConfiguration(conf => conf.AddProfile<TeacherProfile>())
                .CreateMapper();

        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            //var teachers = _context.Teachers
            //    .Include(t => t.Department).ToList();

            var teachers = teacherRepo.GetQueryable()
                .Include(t => t.Department)
                .ToList();

            var DTO = _mapper.Map<List<TeacherDTO>>(teachers);
            
            return Ok(DTO);
        }


        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            //var teacher = _context.Teachers
            //    .Include(t => t.Department)
            //    .FirstOrDefault(t => t.Id == id);

            var teacher = teacherRepo.GetQueryable()
                .Include(t => t.Department)
                .FirstOrDefault(t => t.Id == id);

            if (teacher == null)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }


            var dto = _mapper.Map<TeacherDTO>(teacher);

            return Ok(dto);
        }


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

            teacherRepo.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeacherById), new { id = Entity.Id }, teacher);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTeacher([FromRoute] int id,  [FromBody] UpdateTeacherDTO updatedTeacher)
        {
            if (updatedTeacher == null)
            {
                return BadRequest("Teacher data is null.");
            }

            var teacher = teacherRepo.GetById(id);

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