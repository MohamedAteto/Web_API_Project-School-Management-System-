namespace SchoolProjcet.DTOs.EnrollmentDTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public decimal Grade { get; set; }
    }
}
