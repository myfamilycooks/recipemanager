using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface IRoleService
    {
        Task<RoleDefinition> CreateRole(RoleDefinition role);
        Task<RoleDefinition> GetRoleDefinition(Guid id);
        Task<List<UserRole>> GetRoleMembers(Guid roleId);
        Task<List<RoleDefinition>> GetUserRoles(Guid userId);
        Task<UserRole> GrantUserRole(Guid userid, Guid roleid);
        Task RevokeRoleFromUser(Guid userid, Guid roleid);
        Task<RoleDefinition> UpdateRole(RoleDefinition role);
    }
}