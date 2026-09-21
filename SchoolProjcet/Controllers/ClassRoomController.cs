using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.ClassRoomDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassRoomController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetClassRooms()
        {
            var classrooms = _context.ClassRooms.Select(s => new
            {
                s.Id,
                s.Name,
                s.GradeLevel,
                s.Capacity
            }).ToList();


            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassRoom([FromRoute] int id)
        {
            var classRoom = _context.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return NotFound();
            }

            var DTOitem = new
            {
                classRoom.Id,
                classRoom.Name,
                classRoom.GradeLevel,
                classRoom.Capacity
            };
            return Ok(DTOitem);
        }

        [HttpPost]
        public IActionResult CreateClassRoom([FromBody] CreateClassRoomDTO classRoomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var classRoom = new ClassRoom
            {
                Name = classRoomDTO.Name,
                GradeLevel = classRoomDTO.GradeLevel,
                Capacity = classRoomDTO.Capacity
            };
            _context.ClassRooms.Add(classRoom);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClassRoom), new { id = classRoom.Id }, classRoom);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateClassRoom([FromRoute] int id, [FromBody] UpdateClassRoomDTO classRoomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var classRoom = _context.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return NotFound();
            }
            classRoom.Name = classRoomDTO.Name;
            classRoom.GradeLevel = classRoomDTO.GradeLevel;
            classRoom.Capacity = classRoomDTO.Capacity;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClassRoom([FromRoute] int id)
        {
            var classRoom = _context.ClassRooms.Find(id);
            if (classRoom == null)
            {
                return NotFound();
            }
            _context.ClassRooms.Remove(classRoom);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
