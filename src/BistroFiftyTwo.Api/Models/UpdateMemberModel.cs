using System;
using System.ComponentModel.DataAnnotations;

namespace BistroFiftyTwo.Api.Models
{
    public class UpdateMemberModel
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int MembershipStatus { get; set; }

        [Required]
        public int AccessLevel { get; set; }
    }
}