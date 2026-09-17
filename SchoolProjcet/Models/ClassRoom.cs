using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class ClassRoom
    {
        [Required]
        [MaxLength(50)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(1, 12)]
        public int GradeLevel { get; set; }

        [Range(1, 100)]
        public int Capacity { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
