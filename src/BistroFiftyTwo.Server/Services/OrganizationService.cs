using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;

namespace BistroFiftyTwo.Server.Services
{
    public class OrganizationService : IOrganizationService, IDisposable
    {
        protected IOrganizationRepository Repository { get; set; }
        protected ISecurityService SecurityService { get; set; }
        public OrganizationService(IOrganizationRepository repository, ISecurityService securityService)
        {
            Repository = repository;
            SecurityService = securityService;
        }

        public async Task<Organization> Get(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<Organization> Create(Organization item)
        {
            item.Owner = await SecurityService.GetCurrentUserName();
            item.CreatedBy = await SecurityService.GetCurrentUserName();
            item.CreatedDate = DateTime.UtcNow;
            item.ModifiedBy = await SecurityService.GetCurrentUserName();
            item.ModifiedDate = DateTime.UtcNow;

            return await Repository.CreateAsync(item);
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<Organization> Update(Organization item)
        {
            item.ModifiedBy = await SecurityService.GetCurrentUserName();
            item.ModifiedDate = DateTime.UtcNow;

            return await Repository.UpdateAsync(item);
        }

        public async Task Delete(Organization item)
        {
            await Repository.DeleteAsync(item);
        }

        public async Task<Organization> GetByUrlKeyAsync(string urlKey)
        {
            return await Repository.GetByUrlKeyAsync(urlKey);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}