using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Data
{
    public interface IRecipeIngredientRepository : IDataRepository<RecipeIngredient>
    {
        Task<IEnumerable<RecipeIngredient>> GetByRecipeID(Guid recipeId);
    }
}