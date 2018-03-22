using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Api.Models
{
    public class SecuredUserAccount
    {
        public string UserLogin { get; set; }  
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public Guid ID { get; set; } 
    }
}
