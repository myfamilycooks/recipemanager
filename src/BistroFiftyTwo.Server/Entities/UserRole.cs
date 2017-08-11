using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BistroFiftyTwo.Server.Entities
{
    public class UserRole
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string TableName()
        {
            return "user_roles";
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