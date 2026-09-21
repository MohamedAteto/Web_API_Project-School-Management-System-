using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.EnrollmentDTOs
{
    public class UpdateEnrollmentDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Range(0, 100)]
        public decimal Grade { get; set; }
    }
}
