using School.Models;

namespace SchoolProjcet.Reposatories.Interfaces
{
    public interface IStudentRepo 
    {
        public ICollection<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public Student GetStudentByEmail (string email);
        public void CreateStudent(Student student);
        public void UpdateStudent(int id ,Student student);
        public void DeleteStudent(int id);
        public void SaveChanges();


        public IEnumerable <object> GetCountOfStudentsGroupedByClassRoomId();

    }
}
