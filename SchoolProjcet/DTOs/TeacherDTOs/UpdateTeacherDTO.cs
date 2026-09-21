namespace SchoolProjcet.DTOs.TeacherDTOs
{
    public class UpdateTeacherDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DepartmentName { get; set; }
        public int Salary { get; set; }
    }
}
