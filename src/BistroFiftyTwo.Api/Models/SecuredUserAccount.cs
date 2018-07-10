using System;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Api.Models
{
    public class SecuredUserAccount
    {
        public SecuredUserAccount(UserAccount account)
        {
            UserLogin = account.UserLogin;
            Fullname = account.Fullname;
            Email = account.Email;
            IsLocked = account.IsLocked;
            IsDisabled = account.IsDisabled;
            ID = account.ID;
        }

        public SecuredUserAccount()
        {
        }

        public string UserLogin { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDisabled { get; set; }
        public Guid ID { get; set; }
    }
}