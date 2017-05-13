using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface IUserAccountService : IEntityService<UserAccount>
    {
        Task<UserAccount> GetByLogin(string login);
        Task<UserAccount> GetByEmail(string email);
        Task<UserAccount> Login(string userName, string password);
        Task<UserAccount> Secure(UserAccount item, string newPassword);
    }
}