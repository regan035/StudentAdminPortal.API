using AutoMapper;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Utilities.AfterMaps;

namespace StudentAdminPortal.API.Utilities

{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Student,Entities.Student>().ReverseMap();

            CreateMap<Models.Gender, Gender>().ReverseMap();

            CreateMap<Models.Address, Entities.Address>().ReverseMap();

            CreateMap<UpdateStudentRequest, Models.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentRequest, Models.Student>()
                .AfterMap<AddStudentRequestAfterMap>();


        }
    }
}
