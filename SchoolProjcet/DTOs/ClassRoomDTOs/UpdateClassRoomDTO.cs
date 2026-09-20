using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.ClassRoomDTOs
{
    public class UpdateClassRoomDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(1, 12)]
        public int GradeLevel { get; set; }
        [Range(1, 100)]
        public int Capacity { get; set; }
    }
}
