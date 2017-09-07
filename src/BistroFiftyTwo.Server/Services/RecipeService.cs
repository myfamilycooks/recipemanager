using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using BistroFiftyTwo.Server.Parser;

namespace BistroFiftyTwo.Server.Services
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepository RecipeRepository { get; set; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; set; }
        private IStepRepository StepRepository { get; set; }
        private IRecipeParser RecipeParser { get; set; }
        private ClaimsPrincipal Principal { get; set; }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository, IStepRepository stepRepository, IPrincipal principal)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
            RecipeParser = new RecipeParser(new ParserConfiguration());
            Principal = principal as ClaimsPrincipal;
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
            if (String.IsNullOrEmpty(recipe.Notes))
                recipe.Notes = "No Notes Yet.";

            // TODO: Extract this out to a function of its own.
            if (String.IsNullOrEmpty(recipe.CreatedBy) || string.IsNullOrEmpty(recipe.ModifiedBy))
            {
                recipe.CreatedBy = Principal.Identity.Name;
                recipe.ModifiedBy = Principal.Identity.Name;
            }

            var createdRecipe = await RecipeRepository.CreateAsync(recipe);
            var recipeIngredientTasks = new List<Task<RecipeIngredient>>();
            var recipeStepTasks = new List<Task<Step>>();
            
            recipe.Ingredients.ToList().ForEach(async r =>
            {
                r.RecipeId = createdRecipe.ID;
                if (String.IsNullOrEmpty(r.Notes))
                    r.Notes = $"Notes for ingredient {r.Ordinal}";

                if (String.IsNullOrEmpty(r.Units))
                    r.Units = "items";

                if (String.IsNullOrEmpty(r.CreatedBy) || string.IsNullOrEmpty(r.ModifiedBy))
                {
                    r.CreatedBy = Principal.Identity.Name;
                    r.ModifiedBy = Principal.Identity.Name;
                }

               await RecipeIngredientRepository.CreateAsync(r);

            });

            recipe.Steps.ToList().ForEach(async s =>
            {
                s.RecipeId = createdRecipe.ID;

                if (String.IsNullOrEmpty(s.CreatedBy) || string.IsNullOrEmpty(s.ModifiedBy))
                {
                    s.CreatedBy = Principal.Identity.Name;
                    s.ModifiedBy = Principal.Identity.Name;
                }

                await StepRepository.CreateAsync(s);

 
            });

            // pull the recipe from the db which also will populate the cache.
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
