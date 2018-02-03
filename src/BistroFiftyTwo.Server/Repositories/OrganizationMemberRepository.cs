using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public class OrganizationMemberRepository : IOrganizationMemberRepository
    {
        public async Task<OrganizationMember> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationMember> CreateAsync(OrganizationMember item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrganizationMember>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationMember> UpdateAsync(OrganizationMember item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(OrganizationMember item)
        {
            throw new NotImplementedException();
        }
    }
}