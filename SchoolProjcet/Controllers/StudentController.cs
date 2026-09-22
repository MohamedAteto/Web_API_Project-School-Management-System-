using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
using SchoolProjcet.Mapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudentController()
        {
            _context = new AppDbContext();
            _mapper = new MapperConfiguration(config => config.AddProfile<StudentProfile>())
                .CreateMapper();
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _context.Students.Include(op => op.ClassRoom).Where(s => s.ClassRoomId == 2).ToList();


            if (students is null )
                return BadRequest("Has NO Data");

            var DTO = _mapper.Map<List<StudentDTO>>(students);

            return Ok(DTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _context.Students.Find(id);


            //var student = _context.Students
            //.Include(s => s.ClassRoom)
            //.First(s => s.Id == id);


            //   var student = _context.Students
            //.Include(s => s.ClassRoom)
            //.First(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var DTO = _mapper.Map<StudentDTO>(student);

            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");

            var Entity = _mapper.Map<Student>(studentDTO);

            _context.Students.Add(Entity);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudentById), new { id = Entity.Id }, studentDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute]int id, UpdateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            _mapper.Map(source: studentDTO, destination: student);





            
            //student.FirstName = studentDTO.FirstName;
            //student.LastName = studentDTO.LastName;
            //student.Email = studentDTO.Email;
            //student.PhoneNumber = studentDTO.phoneNumber;
            //student.ClassRoomId = studentDTO.CalssRoomID;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();


            _context.Students.Remove(student);
            _context.SaveChanges();
            return NoContent();
        }




        [HttpGet("ByEmail/{email}")]
        public IActionResult GetStudentByEmail( [FromBody]string email)
        {
            var student = _context.Students.Single(s => s.Email == email);
            if (student == null)
                return NotFound();

            var DTO = _mapper.Map<StudentDTO>(student);
            return Ok(DTO);
        }
    }
}
