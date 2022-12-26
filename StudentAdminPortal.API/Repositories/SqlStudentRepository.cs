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
 

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student
                .Include(nameof(Gender))
                .Include(nameof(Address))
                .ToListAsync();
        }


        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x=>x.Id == studentId);
        }
    }
}
