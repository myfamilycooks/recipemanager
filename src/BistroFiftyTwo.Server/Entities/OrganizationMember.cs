using System;

namespace BistroFiftyTwo.Server.Entities
{
    public class OrganizationMember  
    {
        public Guid OrganizationId { get; set; }
        public Guid AccountId { get; set; }
        public int AccessLevel { get; set; }
    }
}