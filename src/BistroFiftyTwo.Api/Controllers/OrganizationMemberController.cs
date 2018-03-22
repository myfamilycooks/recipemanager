using System;
using System.Net;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/organization/{urlkey}/members")]
    public class OrganizationMemberController : Controller
    {
        public OrganizationMemberController(IOrganizationService organizationService, ISecurityService securityService)
        {
            SecurityService = securityService;
            OrganizationService = organizationService;
        }

        protected IOrganizationService OrganizationService { get; set; }
        protected ISecurityService SecurityService { get; set; }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddMember(string urlKey, [FromBody] AddOrganizationMemberModel model)
        {
            if (model.AccountId.Equals(Guid.Empty)) return BadRequest();

            var organization = await OrganizationService.GetByUrlKeyAsync(urlKey);

            if (organization == null) return BadRequest(BistroFiftyTwoError.Invalid("organization", urlKey));

            var member = await OrganizationService.GetMember(organization.ID, model.AccountId);

            if (member != null) return StatusCode((int) HttpStatusCode.Conflict);

            var createdBy = await SecurityService.GetCurrentUserName();

            var newMember = new OrganizationMember
            {
                OrganizationId = organization.ID,
                AccountId = model.AccountId,
                AccessLevel = 1,
                CreatedBy = createdBy,
                MembershipStatus = 1,
                ModifiedBy = createdBy
            };

            await OrganizationService.AddMember(newMember);

            return Ok();
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> UpdateMember(string urlKey, [FromBody] UpdateMemberModel model)
        {
            if (model.AccountId.Equals(Guid.Empty)) return BadRequest();
            var organization = await OrganizationService.GetByUrlKeyAsync(urlKey);

            if (organization == null) return BadRequest();

            var member = await OrganizationService.GetMember(organization.ID, model.AccountId);

            if (member == null) return BadRequest();

            member.MembershipStatus = model.MembershipStatus;
            member.AccessLevel = model.AccessLevel;

            await OrganizationService.UpdateMember(member);

            return Ok();
        }
    }
}