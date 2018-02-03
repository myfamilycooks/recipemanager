using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IOrganizationRepository : IDataRepository<Organization>
    {
        
    }

    public interface IOrganizationMemberRepository : IDataRepository<OrganizationMember>
    {
    }

    public class OrganizationRepository : IOrganizationRepository
    {
        public async Task<Organization> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> CreateAsync(Organization item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> UpdateAsync(Organization item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Organization item)
        {
            throw new NotImplementedException();
        }
    }

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
