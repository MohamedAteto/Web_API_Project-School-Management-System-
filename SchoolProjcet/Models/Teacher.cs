using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace School.Models
{

    [Index(nameof(Email), IsUnique = true)]
    public class Teacher
    {

        public int Id { get; set; }

        [Required, MaxLength(50)]
        
        public string FirstName { get; set; }

        [Required,MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; }


        public string? PhoneNumber { get; set; }


        [Range(0, int.MaxValue)]
        public int Salary { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
       
        public Department Department { get; set; }
        [JsonIgnore]
        public ICollection<Subject> Subjects { get; set; }
    }
}
