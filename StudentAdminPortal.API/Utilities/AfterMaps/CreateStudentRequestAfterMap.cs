using AutoMapper;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Models;
using DataModels = StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Utilities.AfterMaps
{
    public class CreateStudentRequestAfterMap : IMappingAction<CreateStudentRequest, Models.Student>
    {
        public void Process(CreateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };

        }
    }
}
