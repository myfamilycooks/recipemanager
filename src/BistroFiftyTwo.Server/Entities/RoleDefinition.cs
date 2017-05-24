using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BistroFiftyTwo.Server.Entities
{
    public class RoleDefinition : IBistroEntity
    {
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string TableName()
        {
            return "role_definitions";
        }

        public List<string> Columns()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsAssignableFrom(typeof(IEnumerable<>)) == false)
                .Select(p => p.Name).ToList();
        }
    }
}