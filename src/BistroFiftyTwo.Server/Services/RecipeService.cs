using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Parser;

namespace BistroFiftyTwo.Server.Services
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepository RecipeRepository { get; set; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; set; }
        private IStepRepository StepRepository { get; set; }
        private IRecipeParser RecipeParser { get; set; }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository, IStepRepository stepRepository)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
            RecipeParser = new RecipeParser(new ParserConfiguration());
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

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            var createdRecipe = await RecipeRepository.CreateAsync(recipe);
            var recipeIngredientTasks = new List<Task<RecipeIngredient>>();
            var recipeStepTasks = new List<Task<Step>>();

            recipe.Ingredients.ToList().ForEach(r =>
            {
                r.RecipeId = createdRecipe.ID;
                var task = RecipeIngredientRepository.CreateAsync(r);

                recipeIngredientTasks.Add(task);
            });

            recipe.Steps.ToList().ForEach(s =>
            {
                s.RecipeId = createdRecipe.ID;
                var task = StepRepository.CreateAsync(s);

                recipeStepTasks.Add(task);
            });

            await Task.WhenAll(recipeIngredientTasks);
            await Task.WhenAll(recipeStepTasks);

            return await GetByIdAsync(createdRecipe.ID);
        }

        public async Task<Recipe> GetByKeyAsync(string key)
        {
            var recipe = await RecipeRepository.GetByKeyAsync(key);
            var ingredientTask = PopulateIngredients(recipe);
            var stepTask = PopulateSteps(recipe);

            await Task.WhenAll(ingredientTask, stepTask);

            return recipe;
        }
        
        public async Task<Recipe> ParseAsync(string input)
        {
            return await Task.Run(() =>
            {
                var parseOutput = RecipeParser.Parse(input);
                return parseOutput.Output;
            });
        }


        public Recipe Parse(string input)
        {
            var parseOutput = RecipeParser.Parse(input);
            return parseOutput.Output;
           
        }

        public ParserResult ParseFull(string input)
        {
            return RecipeParser.Parse(input); 
        }
    }
}
