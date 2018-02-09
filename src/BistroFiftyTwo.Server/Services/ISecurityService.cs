using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISecurityService
    {
        Task<UserAccount> GetCurrentUser();
        Task<string> GetCurrentUserName();
    }
}