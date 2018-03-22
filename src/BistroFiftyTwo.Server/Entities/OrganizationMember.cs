using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Entities
{
    public class OrganizationMember : IBistroEntity
    {
        public Guid OrganizationId { get; set; }
        public Guid AccountId { get; set; }
        public int AccessLevel { get; set; }
        public int MembershipStatus { get; set; }
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string TableName()
        {
            return "organization_members";
        }

        public List<string> Columns()
        {
            throw new NotImplementedException();
        }
    }
}