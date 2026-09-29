using School.Models;
using SchoolProjcet.Reposatories.Implmentation;

namespace SchoolProjcet.Reposatories.Interfaces
{
    public interface IStudentRepo : IGenaricRepo<Student>
    {
        public ICollection<Student> GetAllStudents();
        //public Student GetStudentById(int id);
        //public Student GetStudentByEmail (string email);
        //public void CreateStudent(Student student);
        //public void UpdateStudent(int id ,Student student);
        //public void DeleteStudent(int id);
        //public void SaveChanges();


        //public IEnumerable <object> GetCountOfStudentsGroupedByClassRoomId();

    }
}
