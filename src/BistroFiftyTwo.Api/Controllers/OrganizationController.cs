using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/organization")]
    public class OrganizationController : Controller
    {
        protected IOrganizationService OrganizationService { get; set; }

        public OrganizationController(IOrganizationService organizationService)
        {
            OrganizationService = organizationService;
        }

        [Route(""), HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await OrganizationService.GetAll());
        }

        [Route("{id:guid}"), HttpGet]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var org = await OrganizationService.Get(id);
            if (org == null) return NotFound();

            return Ok(org);
        }

        [Route(""), HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationModel createOrganizatinModel)
        {
            if (String.IsNullOrEmpty(createOrganizatinModel.Name)) return BadRequest();
            if (String.IsNullOrEmpty(createOrganizatinModel.Description)) return BadRequest();
            if (String.IsNullOrEmpty(createOrganizatinModel.UrlKey)) return BadRequest();

            var org = new Organization()
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