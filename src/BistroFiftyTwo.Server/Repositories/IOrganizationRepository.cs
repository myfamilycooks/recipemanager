using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IOrganizationRepository : IDataRepository<Organization>, IDisposable
    {
        Task<Organization> GetByUrlKeyAsync(string urlKey);
    }
}