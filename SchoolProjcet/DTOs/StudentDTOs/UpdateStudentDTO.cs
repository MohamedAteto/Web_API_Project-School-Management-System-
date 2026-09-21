using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProjcet.DTOs.StudentDTOs
{
    public class UpdateStudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public int CalssRoomID { get; set; }
        public DateOnly DateOfBirth { get; set; }
        
    }
}
