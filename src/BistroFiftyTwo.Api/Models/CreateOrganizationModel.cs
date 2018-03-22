using System.ComponentModel.DataAnnotations;

namespace BistroFiftyTwo.Api.Models
{
    public class CreateOrganizationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UrlKey { get; set; }
    }
}