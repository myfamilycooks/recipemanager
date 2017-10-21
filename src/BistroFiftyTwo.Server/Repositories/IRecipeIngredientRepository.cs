using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeIngredientRepository : IDataRepository<RecipeIngredient>
    {
        Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId);
    }
}