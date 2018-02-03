using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Entities
{
    public class Organization : IBistroEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
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