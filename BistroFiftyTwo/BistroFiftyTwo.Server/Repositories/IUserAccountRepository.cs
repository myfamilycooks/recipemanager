using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IUserAccountRepository : IDataRepository<UserAccount>, IDisposable
    {
        Task<UserAccount> GetByLoginAsync(string login);
        Task<UserAccount> GetByEmailAsync(string email);
    }
}