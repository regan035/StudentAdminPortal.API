using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly AppDBContext context;

        public SqlStudentRepository(AppDBContext context)
        {
            this.context = context;
        }
        public List<Student> GetStudents()
        {
           return context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToList();
        }
    }
}
