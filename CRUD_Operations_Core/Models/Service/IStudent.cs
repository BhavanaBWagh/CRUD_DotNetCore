namespace CRUD_Operations_Core.Models.Service
{
    public interface IStudentServices
    {
        List<Student> GetStudents();

        List<Student> SaveStudents(List<Student> students);
    }
}
