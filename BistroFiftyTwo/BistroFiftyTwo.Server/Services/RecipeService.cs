using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetByIdAsync(Guid id);
        Task<Recipe> GetByKeyAsync(string key);
    }
    public class RecipeService : IRecipeService
    {
        private IRecipeRepository RecipeRepository { get; set; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; set; }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var recipe = await RecipeRepository.GetAsync(id);
            await PopulateIngredients(recipe);
            return recipe;
        }

        private async Task PopulateIngredients(Recipe recipe)
        {
            recipe.Ingredients = await RecipeIngredientRepository.GetByRecipeIdAsync(recipe.ID);
        }

        public async Task<Recipe> GetByKeyAsync(string key)
        {
            return await RecipeRepository.GetByKeyAsync(key);
        }
    }
}
