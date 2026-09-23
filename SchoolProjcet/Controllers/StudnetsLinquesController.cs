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
    public class StudnetsLinquesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StudnetsLinquesController()
        {
            _context = new AppDbContext();
            _mapper = new MapperConfiguration(config => config.AddProfile<StudentProfile>())
                .CreateMapper();
        }


        [HttpGet("Studentsinuniqclassthathaveenrollment")]
        public IActionResult GetStudentsInUniqueClassThatHaveEnrollment(int minimumGrade , int ClassRoomID)
        {
            var students = _context.Students
                .Include(s => s.ClassRoom)
                .Where(s => s.ClassRoomId == ClassRoomID)
                .Select(m => new
                {
                    m.FirstName,
                    m.ClassRoom.GradeLevel,
                    ClassRoomName = m.ClassRoom.Name,

                }).Any(s => s.GradeLevel >= minimumGrade);


            //var studentDTOs = _mapper.Map<List<StudentDTO>>(students);
            return Ok(students);
        }


        [HttpGet("Get_First_Studnet_InClassRoomID")]
        public IActionResult GetFirstStudentInClassRoom(int ClassRoomID)
        {

            var Students = _context.Students
                .OrderBy(s => s.Id)
                .Where(s => s.ClassRoomId == ClassRoomID)
                .Select(m => new
                {
                    m.FirstName,
                    m.LastName,
                    m.ClassRoomId
                })
                .ToList();

            return Ok(Students);
        }



    }
}
