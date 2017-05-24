using BistroFiftyTwo.Server.Entities;
using System;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeRepository : IDataRepository<Recipe>, IDisposable
    {
        Task<Recipe> GetByKeyAsync(string key);       
    }
}
