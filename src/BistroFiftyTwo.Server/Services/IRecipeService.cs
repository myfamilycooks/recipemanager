using BistroFiftyTwo.Server.Entities;
using System;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetByIdAsync(Guid id);
        Task<Recipe> GetByKeyAsync(string key);
    }
}
