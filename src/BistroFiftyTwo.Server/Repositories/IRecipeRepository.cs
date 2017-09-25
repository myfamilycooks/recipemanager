using BistroFiftyTwo.Server.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeRepository : IDataRepository<Recipe>, IDisposable
    {
        Task<Recipe> GetByKeyAsync(string key);       
    }

    public interface IRecipeHistoryRepository : IDataRepository<RecipeHistory>, IDisposable
    {
        Task<IEnumerable<RecipeHistory>> GetHistoryForRecipe(Guid recipeId);
    }
}
