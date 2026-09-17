using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        
        public Student Student { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }

         [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
