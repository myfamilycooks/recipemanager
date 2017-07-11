using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IUserRoleRepository : IDisposable
    {
        Task<IEnumerable<UserRole>> GetByUserIDAsync(Guid userId);
        Task<IEnumerable<UserRole>> GetByRoleIDAsync(Guid roleId);
        Task<UserRole> CreateAsync(UserRole item);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole> UpdateAsync(UserRole item);
        Task DeleteAsync(UserRole item);
        Task<UserRole> GetUserRoleAsync(Guid userid, Guid roleId);
    }
}