using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
using SchoolProjcet.Mapping;
using SchoolProjcet.Reposatories.Implmentation;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepo _studentrepo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public StudentController(StudentRepo context , AppDbContext _context)
        {
            _studentrepo = context;
            _mapper = new MapperConfiguration(config => config.AddProfile<StudentProfile>())
                .CreateMapper();
            this._context = _context;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {

            //var students = _studentrepo
            //    .GetQueryable()
            //    .Include(c => c.ClassRoom).Select(n => new
            //    {
            //        n.FirstName,
            //        n.Email,
            //        n.PhoneNumber,
            //        ClassRoomName = n.ClassRoom.Name
            //    });

            var students = _studentrepo.GetAllStudents()
                .Select(n => new
                {
                    n.FirstName,
                    n.Email,
                    n.PhoneNumber,
                    ClassRoomName = n.ClassRoom.Name
                }).ToList();


            if (students is null )
                return BadRequest("Has NO Data");

            //var DTO = _mapper.Map<List<StudentDTO>>(students);

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            //var student = _studentrepo.GetById(id);

            var student = _studentrepo
                .GetQueryable()
                .Include(c => c.ClassRoom)
                .FirstOrDefault(n => n.Id == id);
             

            if (student == null)
            {
                return NotFound();
            }

            var DTO = _mapper.Map<StudentDTO>(student);

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");

            var Entity = _mapper.Map<Student>(studentDTO);

            _studentrepo.Add(Entity);
            return CreatedAtAction(nameof(GetStudentById), new { id = Entity.Id }, studentDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute]int id, UpdateStudentDTO studentDTO)
        {

            if(studentDTO is null)
                return BadRequest("Student data is null.");

            var student = _studentrepo.GetById(id);
            if (student == null)
                return NotFound();

            _mapper.Map(source: studentDTO, destination: student);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _studentrepo.GetById(id);
            if (student == null)
                return NotFound();


            _studentrepo.Delete(id);
            return NoContent();
        }




        //[HttpGet("ByEmail/{email}")]
        //public IActionResult GetStudentByEmail( [FromBody]string email)
        //{

        //    var student = _context.GetStudentByEmail(email);

        //    if (student == null)
        //        return NotFound();

        //    var DTO = _mapper.Map<StudentDTO>(student);
        //    return Ok(DTO);
        //}



        //[HttpGet("countofstudnetsgroubedbyaspecificclassroomid")]
        //public IActionResult GetCountOfStudentsGroupedByClassRoomId()
        //{

        //    var count = _studentrepo.GetCountOfStudentsGroupedByClassRoomId();

        //    if(count is null)
        //        return NotFound();

        //    return Ok(count);
        //}

    }
}
