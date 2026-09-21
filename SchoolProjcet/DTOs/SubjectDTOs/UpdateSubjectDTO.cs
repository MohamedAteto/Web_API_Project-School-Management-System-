using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using School.Models;

namespace SchoolProjcet.DTOs.SubjectDTOs
{
    public class UpdateSubjectDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MaxGrade { get; set; }
        public int TeacherId { get; set; }

    }
}
