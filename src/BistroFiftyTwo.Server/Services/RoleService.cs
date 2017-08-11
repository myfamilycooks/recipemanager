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
        public RoleService(IRoleDefinitionRepository roleDefinitionRepository, IUserRoleRepository userRoleRepository,
            IUserAccountService userAccountService)
        {
            RoleDefinitionRepository = roleDefinitionRepository;
            UserRoleRepository = userRoleRepository;
            UserAccountService = userAccountService;
        }

        protected IRoleDefinitionRepository RoleDefinitionRepository { get; set; }
        protected IUserRoleRepository UserRoleRepository { get; set; }
        protected IUserAccountService UserAccountService { get; set; }

        public async Task<RoleDefinition> GetRoleDefinition(Guid id)
        {
            return await RoleDefinitionRepository.GetAsync(id);
        }

        public async Task<List<RoleDefinition>> GetUserRoles(Guid userId)
        {
            var userRoles = await UserRoleRepository.GetByUserIDAsync(userId);
            var taskList = new List<Task<RoleDefinition>>();
            userRoles.ToList().ForEach(x => { taskList.Add(GetRoleDefinition(x.RoleID)); });

            var rolesForUser = await Task.WhenAll(taskList);
            return rolesForUser.ToList();
        }

        public async Task<List<UserRole>> GetRoleMembers(Guid roleId)
        {
            ///TODO: Add Caching.
            var users = await UserRoleRepository.GetByRoleIDAsync(roleId);
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

        public async Task<UserRole> GrantUserRole(Guid userid, Guid roleid)
        {
            var userRole = await UserRoleRepository.GetUserRoleAsync(userid, roleid);

            if (userRole != null)
                return userRole;
            userRole = new UserRole
            {
                UserID = userid,
                RoleID = roleid,
                IsDisabled = false,
                CreatedBy = "chef",
                ModifiedBy = "chef",
                ModifiedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            return await UserRoleRepository.CreateAsync(userRole);
        }

        public async Task RevokeRoleFromUser(Guid userid, Guid roleid)
        {
            await UserRoleRepository.DeleteAsync(new UserRole {UserID = userid, RoleID = roleid});
        }
    }
}