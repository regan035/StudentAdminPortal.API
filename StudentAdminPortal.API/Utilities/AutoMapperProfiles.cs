using AutoMapper;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Utilities.AfterMaps;

namespace StudentAdminPortal.API.Utilities

{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Student,Entities.Student>();
            CreateMap<Models.Gender, Entities.Gender>();
            CreateMap<Models.Address, Entities.Address>();
            CreateMap<UpdateStudentRequest, Models.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();


        }
    }
}
