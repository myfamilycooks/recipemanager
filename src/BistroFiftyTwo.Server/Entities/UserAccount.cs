using System;
using System.Collections.Generic;
using System.Linq;

namespace BistroFiftyTwo.Server.Entities
{
    public class UserAccount : IBistroEntity
    {
        public string UserLogin { get; set; }
        public string AccountPassword { get; set; }
        public string Salt { get; set; }
        public int PasswordFormat { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string TableName()
        {
            return "user_accounts";
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