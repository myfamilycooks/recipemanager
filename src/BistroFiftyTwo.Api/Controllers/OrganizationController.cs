using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/organization")]
    public class OrganizationController : Controller
    {
        public OrganizationController(IOrganizationService organizationService)
        {
            OrganizationService = organizationService;
        }

        protected IOrganizationService OrganizationService { get; set; }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await OrganizationService.GetAll());
        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var org = await OrganizationService.Get(id);
            if (org == null) return NotFound();

            return Ok(org);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationModel createOrganizatinModel)
        {
            if (string.IsNullOrEmpty(createOrganizatinModel.Name))
                return BadRequest(BistroFiftyTwoError.MissingField("name"));
            if (string.IsNullOrEmpty(createOrganizatinModel.Description))
                return BadRequest(BistroFiftyTwoError.MissingField("description"));
            if (string.IsNullOrEmpty(createOrganizatinModel.UrlKey))
                return BadRequest(BistroFiftyTwoError.MissingField("urlKey"));

            var org = new Organization
            {
                Name = createOrganizatinModel.Name,
                Description = createOrganizatinModel.Description,
                UrlKey = createOrganizatinModel.UrlKey,
                OrgType = 1
            };

            var created = await OrganizationService.Create(org);

            if (created == null) return BadRequest();

            return Ok(created);
        }
    }
}