using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        //return all students
        Task<List<Student>> GetStudentsAsync();

        //return one single student
        Task<Student> GetStudentAsync(Guid studentId);
    }
}
