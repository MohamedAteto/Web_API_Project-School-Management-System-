using School.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.SubjectDTOs
{
    public class SubjectDTO
    {   
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public int MaxGrade { get; set; }
       
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

    }
}
