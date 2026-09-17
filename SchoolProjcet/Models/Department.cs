using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Teacher>Teachers { get; set; } 
    }
}
