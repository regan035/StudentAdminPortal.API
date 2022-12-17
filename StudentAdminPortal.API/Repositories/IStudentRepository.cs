﻿using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
       Task<List<Student>> GetStudentsAsync();
    }
}
