using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface IOrganizationService : IEntityService<Organization>
    {
        Task<Organization> GetByUrlKeyAsync(string urlKey);
    }
}