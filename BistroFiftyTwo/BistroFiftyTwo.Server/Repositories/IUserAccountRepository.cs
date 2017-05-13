using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IUserAccountRepository : IDataRepository<UserAccount>, IDisposable
    {
        Task<UserAccount> GetByLogin(string login);
        Task<UserAccount> GetByEmail(string email);
    }
}