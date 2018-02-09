using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IOrganizationMemberRepository : IDataRepository<OrganizationMember>
    {
        Task<OrganizationMember> GetAsync(Guid organizationId, Guid accountId);
    }
}