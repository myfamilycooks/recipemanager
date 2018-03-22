using System.ComponentModel.DataAnnotations;

namespace BistroFiftyTwo.Api.Models
{
    public class CreateAccountModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string InvitationCode { get; set; }
    }
}