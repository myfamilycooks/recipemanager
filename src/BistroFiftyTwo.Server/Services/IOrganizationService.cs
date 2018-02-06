using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface IOrganizationService : IEntityService<Organization>
    {
        Task<Organization> GetByUrlKeyAsync(string urlKey);
        Task AddMember(OrganizationMember member);
        Task UpdateMember(OrganizationMember member);
        Task<OrganizationMember> GetMember(Guid organizationId, Guid accountId);
    }
}