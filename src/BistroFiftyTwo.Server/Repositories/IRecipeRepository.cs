using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeRepository : IDataRepository<Recipe>, IDisposable
    {
        Task<Recipe> GetByKeyAsync(string key);
        Task<IEnumerable<Recipe>> Search(string query);
    }
}