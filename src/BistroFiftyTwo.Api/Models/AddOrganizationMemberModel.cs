using System;
using System.ComponentModel.DataAnnotations;

namespace BistroFiftyTwo.Api.Models
{
    public class AddOrganizationMemberModel
    {
        [Required] public string Reason { get; set; }
        [Required] public Guid AccountId { get; set; }
    }
}