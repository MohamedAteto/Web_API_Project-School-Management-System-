using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.Controllers;
using SchoolProjcet.Reposatories.Interfaces;

namespace SchoolProjcet.Reposatories.Implmentation
{
    public class StudentRepo : GanaricRepo<Student>, IStudentRepo
    {
        private readonly AppDbContext _context;
        public StudentRepo(AppDbContext context) : base(context)
        {

            _context = context;
        }

        public ICollection<Student> GetAllStudents()
        {
            return _context.Students
                .Include(c => c.ClassRoom)
                .ToList();
        }























        //public void CreateStudent(Student student)
        //{
        //   if(student == null)
        //        throw new ArgumentNullException(nameof(student));

        //   _context.Add(student);
        //    SaveChanges();
        //}

        //public void DeleteStudent(int id)
        //{
        //    var item  = _context.Students.Find(id);
        //    if (item == null)
        //        throw new ArgumentNullException(nameof(item));

        //    _context.Students.Remove(item);
        //    SaveChanges();
        //}

        //public ICollection<Student> GetAllStudents()
        //{
        //    var items = _context.Students.ToList();

        //    if(items == null || items.Count == 0)
        //        throw new ArgumentNullException(nameof(items));

        //    return items;
        //}

        //public IEnumerable<object> GetCountOfStudentsGroupedByClassRoomId()
        //{
        //    var result = _context.Students
        //        .GroupBy(s => s.ClassRoomId)
        //        .Select(g => new
        //        {
        //            ClassRoomId = g.Key,
        //            StudentCount = g.Count(),
        //            StudentNames = g.Select(n => n.FirstName + " " + n.LastName).ToList()
        //        })
        //        .ToList();

        //    return result;
        //}


        //public Student GetStudentByEmail(string email)
        //{
        //    var student = _context.Students.Single(s => s.Email == email);

        //    if (student == null)
        //        throw new ArgumentNullException(nameof(student));

        //    return student;
        //}

        //public Student GetStudentById(int id)
        //{
        //    var item = _context.Students.Find(id);

        //    if (item == null)
        //        throw new ArgumentNullException(nameof(item));

        //    return item;
        //}

        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}

        //public void UpdateStudent(int id, Student student)
        //{
        //    var item = _context.Students.Find(id);

        //    if (item == null)
        //        throw new ArgumentNullException(nameof(item));

        //    item.FirstName = student.FirstName;
        //    item.LastName = student.LastName;
        //    item.Email = student.Email;
        //    item.PhoneNumber = student.PhoneNumber;
        //    item.DateOfBirth = student.DateOfBirth;

        //    _context.Students.Update(item);
        //    SaveChanges();
        //}
    }
}
