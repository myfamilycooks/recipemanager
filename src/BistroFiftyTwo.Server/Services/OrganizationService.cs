using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;

namespace BistroFiftyTwo.Server.Services
{
    public class OrganizationService : IOrganizationService, IDisposable
    {
        public OrganizationService(IOrganizationRepository repository,
            IOrganizationMemberRepository organizationMemberRepository, ISecurityService securityService,
            ICacheService cacheService)
        {
            Repository = repository;
            MemberRepository = organizationMemberRepository;
            SecurityService = securityService;
            CacheService = cacheService;
        }

        protected IOrganizationRepository Repository { get; set; }
        protected IOrganizationMemberRepository MemberRepository { get; set; }
        protected ISecurityService SecurityService { get; set; }
        protected ICacheService CacheService { get; set; }

        public void Dispose()
        {
            Repository.Dispose();
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
            var org = await CacheService.GetAsync<Organization>($"ORG${urlKey}");

            if (org != null) return org;

            org = await Repository.GetByUrlKeyAsync(urlKey);
            CacheService.SetAsync($"ORG${urlKey}", org, 1024 * 30);

            return org;
        }

        public async Task AddMember(OrganizationMember member)
        {
            member.CreatedBy = await SecurityService.GetCurrentUserName();
            member.ModifiedBy = await SecurityService.GetCurrentUserName();
            member.CreatedDate = DateTime.UtcNow;
            member.ModifiedDate = DateTime.UtcNow;

            await MemberRepository.CreateAsync(member);
        }

        public async Task UpdateMember(OrganizationMember member)
        {
            member.ModifiedBy = await SecurityService.GetCurrentUserName();
            member.ModifiedDate = DateTime.UtcNow;

            await MemberRepository.UpdateAsync(member);
        }

        public async Task<OrganizationMember> GetMember(Guid organizationId, Guid accountId)
        {
            return await MemberRepository.GetAsync(organizationId, accountId);
        }
    }
}