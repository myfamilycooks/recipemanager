using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;

namespace BistroFiftyTwo.Server.Services
{
    public class RoleService : IRoleService
    {
        public RoleService(IRoleDefinitionRepository roleDefinitionRepository,
            IAccountRoleRepository accountRoleRepository,
            IUserAccountService userAccountService)
        {
            RoleDefinitionRepository = roleDefinitionRepository;
            AccountRoleRepository = accountRoleRepository;
            UserAccountService = userAccountService;
        }

        protected IRoleDefinitionRepository RoleDefinitionRepository { get; set; }
        protected IAccountRoleRepository AccountRoleRepository { get; set; }
        protected IUserAccountService UserAccountService { get; set; }

        public async Task<RoleDefinition> GetRoleDefinition(Guid id)
        {
            return await RoleDefinitionRepository.GetAsync(id);
        }

        public async Task<List<RoleDefinition>> GetUserRoles(Guid userId)
        {
            var userRoles = await AccountRoleRepository.GetByAccountIdAsync(userId);
            var taskList = new List<Task<RoleDefinition>>();
            userRoles.ToList().ForEach(x => { taskList.Add(GetRoleDefinition(x.RoleID)); });

            var rolesForUser = await Task.WhenAll(taskList);
            return rolesForUser.ToList();
        }

        public async Task<List<AccountRole>> GetRoleMembers(Guid roleId)
        {
            ///TODO: Add Caching.
            var users = await AccountRoleRepository.GetByRoleIDAsync(roleId);
            return users.ToList();
        }

        public async Task<RoleDefinition> CreateRole(RoleDefinition role)
        {
            return await RoleDefinitionRepository.CreateAsync(role);
        }

        public async Task<RoleDefinition> UpdateRole(RoleDefinition role)
        {
            return await RoleDefinitionRepository.UpdateAsync(role);
        }

        public async Task<AccountRole> GrantUserRole(Guid userid, Guid roleid)
        {
            var userRole = await AccountRoleRepository.GetUserRoleAsync(userid, roleid);

            if (userRole != null)
                return userRole;
            userRole = new AccountRole
            {
                AccountID = userid,
                RoleID = roleid,
                CreatedBy = "chef",
                ModifiedBy = "chef",
                ModifiedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            return await AccountRoleRepository.CreateAsync(userRole);
        }

        public async Task RevokeRoleFromUser(Guid userid, Guid roleid)
        {
            await AccountRoleRepository.DeleteAsync(new AccountRole {AccountID = userid, RoleID = roleid});
        }

        public async Task GrantDefaultRoles(Guid userAccountId)
        {
            var roles = await RoleDefinitionRepository.GetAllAsync();
            var defaultRole = roles.FirstOrDefault(r => r.Name == "authenticated");

            await GrantUserRole(userAccountId, defaultRole.ID);
        }
    }
}