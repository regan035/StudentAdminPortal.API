using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        //return all students
        Task<List<Student>> GetStudentsAsync();

        //return one single student
        Task<Student> GetStudentAsync(Guid studentId);

        Task<List<Gender>>GetGendersAsync();
        
        Task<bool>Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId,Student request);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> CreateStudent(Student request);
    }
}
