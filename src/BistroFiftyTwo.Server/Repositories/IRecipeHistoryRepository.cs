using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeHistoryRepository : IDataRepository<RecipeHistory>, IDisposable
    {
        Task<IEnumerable<RecipeHistory>> GetHistoryForRecipe(Guid recipeId);
        RecipeHistory Create(RecipeHistory history);
    }
}