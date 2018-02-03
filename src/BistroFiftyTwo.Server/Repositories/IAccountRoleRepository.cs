using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IAccountRoleRepository : IDisposable
    {
        Task<IEnumerable<AccountRole>> GetByAccountIdAsync(Guid accountId);
        Task<IEnumerable<AccountRole>> GetByRoleIDAsync(Guid roleId);
        Task<AccountRole> CreateAsync(AccountRole item);
        Task<IEnumerable<AccountRole>> GetAllAsync();
        Task<AccountRole> UpdateAsync(AccountRole item);
        Task DeleteAsync(AccountRole item);
        Task<AccountRole> GetUserRoleAsync(Guid userid, Guid roleId);
    }
}