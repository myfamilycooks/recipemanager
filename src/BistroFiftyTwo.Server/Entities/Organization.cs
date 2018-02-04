using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace BistroFiftyTwo.Server.Entities
{
    [Table("organizations")]
    public class Organization : IBistroEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UrlKey { get; set; }
        public string Description { get; set; }
        public int OrgType { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string TableName()
        {
            throw new NotImplementedException();
        }

        public List<string> Columns()
        {
            throw new NotImplementedException();
        }
    }
}