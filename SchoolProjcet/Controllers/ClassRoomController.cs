using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.ClassRoomDTOs;
using SchoolProjcet.Mapping;
using SchoolProjcet.Reposatories.Implmentation;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGenaricRepo<ClassRoom> genaricRepo;
        private readonly IMapper _mapper;
        public ClassRoomController(AppDbContext context , IGenaricRepo<ClassRoom> genaricRepo)
        {
            _context = context;
            this.genaricRepo = genaricRepo;
            _mapper = new MapperConfiguration(config => config.AddProfile<ClassRoomProfile>())
                .CreateMapper();
        }

        [HttpGet]
        public IActionResult GetClassRooms()
        {
            var classrooms = genaricRepo.GetAll();
            if (classrooms is null || classrooms.Count == 0)
                return BadRequest("Has No Data");

            var DTO = _mapper.Map<List<ClassRoomDTO>>(classrooms);
            return Ok(DTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassRoom([FromRoute] int id)
        {
            var classRoom = genaricRepo.GetById(id);
            if (classRoom == null)
            {
                return NotFound();
            }
            var DTO = _mapper.Map<ClassRoomDTO>(classRoom);

            return Ok(DTO);
        }

        [HttpPost]
        public IActionResult CreateClassRoom([FromBody] CreateClassRoomDTO classRoomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Entity = _mapper.Map<ClassRoom>(classRoomDTO);
            
            genaricRepo.Add(Entity);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClassRoom), new { id = Entity.Id }, classRoomDTO);
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

            _mapper.Map(source: classRoomDTO, destination: classRoom);
            //genaricRepo.Update(classRoom);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClassRoom([FromRoute] int id)
        {
            var classRoom = genaricRepo.GetById(id);
            if (classRoom == null)
            {
                return NotFound();
            }
            genaricRepo.Delete(id);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
