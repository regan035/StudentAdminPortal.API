﻿namespace StudentAdminPortal.API.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }

        //Navigation Property
        public Guid StudentId { get; set; }

        public static implicit operator Address(Models.Address v)
        {
            throw new NotImplementedException();
        }
    }
}
