using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeManager.WebApp.Data;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetRecipeByID(Guid id);
        Task<Recipe> GetRecipeByKey(string key);
    }
    public class RecipeService : IRecipeService
    {
        private IRecipeRepository RecipeRepository { get; set; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; set; }
        private IStepRepository StepRepository { get; set; }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository,
            IStepRepository stepRepository)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
        }

        public async Task<Recipe> GetRecipeByID(Guid id)
        {
            var recipe = await RecipeRepository.Get(id);
            recipe.Ingredients = await RecipeIngredientRepository.GetByRecipeID(recipe.Id);
            recipe.Steps = await StepRepository.GetByRecipeID(recipe.Id);

            return recipe;
        }

        public async Task<Recipe> GetRecipeByKey(string key)
        {
            var recipe = await RecipeRepository.GetByKey(key);
            recipe.Ingredients = await RecipeIngredientRepository.GetByRecipeID(recipe.Id);
            recipe.Steps = await StepRepository.GetByRecipeID(recipe.Id);

            return recipe;
        }
    }
}
