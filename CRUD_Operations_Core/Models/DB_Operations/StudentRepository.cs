using CRUD_Operations_Core.Data;
using CRUD_Operations_Core.Models.Service;
using EFCore.BulkExtensions;

namespace CRUD_Operations_Core.Models.DB_Operations
{
    public class StudentRepository : IStudentServices
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

      

        public List<Student> SaveStudents(List<Student> students)
        {
            _context.BulkInsert(students);
            return students;    
        }
    }
}
