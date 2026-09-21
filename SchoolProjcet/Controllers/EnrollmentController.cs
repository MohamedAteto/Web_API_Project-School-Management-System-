using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.EnrollmentDTOs;
using SchoolProjcet.Mapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnrollmentController()
        {
            _context = new AppDbContext();

            _mapper = new MapperConfiguration(config =>
                config.AddProfile<EnrollmentProfile>())
                .CreateMapper();
        }

        [HttpGet]
        public IActionResult GetEnrollments()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ToList();

            if (enrollments is null || enrollments.Count == 0)
            {
                return BadRequest("Has No Data");
            }

            var DTO = _mapper.Map<List<EnrollmentDTO>>(enrollments);

            return Ok(DTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetEnrollment([FromRoute] int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
                return NotFound();
            

            var DTO = _mapper.Map<EnrollmentDTO>(enrollment);

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateEnrollment( [FromBody] CreateEnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var Entity = _mapper.Map<Enrollment>(enrollmentDTO);

            _context.Enrollments.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetEnrollment),
                new { id = Entity.Id },
                enrollmentDTO
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEnrollment( [FromRoute] int id, [FromBody] UpdateEnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var enrollment = _context.Enrollments.Find(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            _mapper.Map(
                source: enrollmentDTO,
                destination: enrollment
            );

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment([FromRoute] int id)
        {
            var enrollment = _context.Enrollments.Find(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
