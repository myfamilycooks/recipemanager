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
        private IStepRepository StepRepository { get; set; }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository, IStepRepository stepRepository)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var recipe = await RecipeRepository.GetAsync(id);
            var ingredientTask = PopulateIngredients(recipe);
            var stepTask = PopulateSteps(recipe);

            await Task.WhenAll(ingredientTask, stepTask);
            
            return recipe;
        }

        private async Task PopulateSteps(Recipe recipe)
        {
            recipe.Steps = await StepRepository.GetByRecipeIdAsync(recipe.ID);
        }

        private async Task PopulateIngredients(Recipe recipe)
        {
            recipe.Ingredients = await RecipeIngredientRepository.GetByRecipeIdAsync(recipe.ID);
        }

        public async Task<Recipe> GetByKeyAsync(string key)
        {
            var recipe = await RecipeRepository.GetByKeyAsync(key);
            var ingredientTask = PopulateIngredients(recipe);
            var stepTask = PopulateSteps(recipe);

            await Task.WhenAll(ingredientTask, stepTask);

            return recipe;
        }
    }
}
