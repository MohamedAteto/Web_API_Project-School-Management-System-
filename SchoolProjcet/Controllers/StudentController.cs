using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _context.Students.Select(e => new StudentDTO
            {
                
                FullName = e.FirstName + " " + e.LastName,
                Email = e.Email,
                phoneNumber = e.PhoneNumber,
                CalssRoomName = e.ClassRoom.Name

            }).ToList();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            var DTO = new StudentDTO
            {
                FullName = student.FirstName + " " + student.LastName,
                Email = student.Email,
                phoneNumber = student.PhoneNumber,
                CalssRoomName = student.ClassRoom.Name
            };
            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");


            var student = new Student
            {
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Email = studentDTO.Email,
                PhoneNumber = studentDTO.phoneNumber,
                ClassRoomId = studentDTO.CalssRoomID
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute]int id, UpdateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            
            student.FirstName = studentDTO.FirstName;
            student.LastName = studentDTO.LastName;
            student.Email = studentDTO.Email;
            student.PhoneNumber = studentDTO.phoneNumber;
            student.ClassRoomId = studentDTO.CalssRoomID;

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
    }
}
