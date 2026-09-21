using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.SubjectDTOs;
using SchoolProjcet.Mapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubjectController()
        {
            _context = new AppDbContext();

            _mapper = new MapperConfiguration(config =>config.AddProfile<SubjectProfile>())
                .CreateMapper();
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _context.Subjects
                .Include(s => s.Teacher)
                .ToList();

            if (subjects is null || subjects.Count == 0)
            {
                return BadRequest("Has No Data");
            }

            var DTO = _mapper.Map<List<SubjectDTO>>(subjects);

            return Ok(DTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetSubject([FromRoute] int id)
        {
            var subject = _context.Subjects
                .Include(s => s.Teacher)
                .FirstOrDefault(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            var DTO = _mapper.Map<SubjectDTO>(subject);

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateSubject(
            [FromBody] CreateSubjectDTO subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Entity = _mapper.Map<Subject>(subjectDTO);

            _context.Subjects.Add(Entity);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetSubject),
                new { id = Entity.Id },
                subjectDTO
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSubject(
            [FromRoute] int id,
            [FromBody] UpdateSubjectDTO subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = _context.Subjects.Find(id);

            if (subject == null)
            {
                return NotFound();
            }

            _mapper.Map(
                source: subjectDTO,
                destination: subject
            );

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubject([FromRoute] int id)
        {
            var subject = _context.Subjects.Find(id);

            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
