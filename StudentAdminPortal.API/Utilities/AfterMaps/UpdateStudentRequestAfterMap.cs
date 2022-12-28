﻿using AutoMapper;
using StudentAdminPortal.API.Entities;
using DataModels = StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Utilities.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
    {
        public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAddress= source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }


    }
}
