using BistroFiftyTwo.Server.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeIngredientRepository : IDataRepository<RecipeIngredient>
    {
        Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId);
    }
}
