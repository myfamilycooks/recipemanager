using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Api.Models
{
    public class CreateAccountModel
    {
        [Required]
        public string Login { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string InvitationCode { get; set; }
    }

    public class CreateOrganizationModel
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string UrlKey { get; set; }
    }
}
