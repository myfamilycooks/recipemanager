using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser;
using BistroFiftyTwo.Server.Repositories;

namespace BistroFiftyTwo.Server.Services
{
    public class RecipeService : IRecipeService
    {
        public RecipeService(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository,
            IStepRepository stepRepository, IRecipeHistoryRepository recipeHistoryRepository, IPrincipal principal)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
            RecipeHistoryRepository = recipeHistoryRepository;
            RecipeParser = new RecipeParser(new ParserConfiguration());
            Principal = principal as ClaimsPrincipal;
        }

        private IRecipeRepository RecipeRepository { get; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; }
        private IStepRepository StepRepository { get; }
        private IRecipeHistoryRepository RecipeHistoryRepository { get; }
        private IRecipeParser RecipeParser { get; }
        private ClaimsPrincipal Principal { get; }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var recipe = await RecipeRepository.GetAsync(id);
            var ingredientTask = PopulateIngredients(recipe);
            var stepTask = PopulateSteps(recipe);

            await Task.WhenAll(ingredientTask, stepTask);

            return recipe;
        }

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            if (string.IsNullOrEmpty(recipe.Notes))
                recipe.Notes = "No Notes Yet.";

            // TODO: Extract this out to a function of its own.
            if (string.IsNullOrEmpty(recipe.CreatedBy) || string.IsNullOrEmpty(recipe.ModifiedBy))
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
                if (string.IsNullOrEmpty(r.Notes))
                    r.Notes = $"Notes for ingredient {r.Ordinal}";

                if (string.IsNullOrEmpty(r.Units))
                    r.Units = "items";

                if (string.IsNullOrEmpty(r.CreatedBy) || string.IsNullOrEmpty(r.ModifiedBy))
                {
                    r.CreatedBy = Principal.Identity.Name;
                    r.ModifiedBy = Principal.Identity.Name;
                }

                await RecipeIngredientRepository.CreateAsync(r);
            });

            recipe.Steps.ToList().ForEach(async s =>
            {
                s.RecipeId = createdRecipe.ID;

                if (string.IsNullOrEmpty(s.CreatedBy) || string.IsNullOrEmpty(s.ModifiedBy))
                {
                    s.CreatedBy = Principal.Identity.Name;
                    s.ModifiedBy = Principal.Identity.Name;
                }

                await StepRepository.CreateAsync(s);
            });

            // set the historical version correctly. - may need to do more work here for revisions, set version correctly.
            var recipeHistory = await RecipeHistoryRepository.GetAsync(recipe.FullTextReference);
            recipeHistory.RecipeID = createdRecipe.ID;
            await RecipeHistoryRepository.UpdateAsync(recipeHistory);

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
            return await Task.Run(() => { return Parse(input); });
        }


        public Recipe Parse(string input)
        {
            var parseOutput = RecipeParser.Parse(input);

            var recipeHistory = new RecipeHistory
            {
                FullText = input,
                CreatedBy = Principal.Identity.Name,
                ModifiedBy = Principal.Identity.Name,
                Version = 1
            };

            var history = RecipeHistoryRepository.Create(recipeHistory);
            parseOutput.Output.FullTextReference = history.ID;

            return parseOutput.Output;
        }

        public ParserResult ParseFull(string input)
        {
            var output = RecipeParser.Parse(input);
            var recipeHistory = new RecipeHistory
            {
                FullText = input,
                CreatedBy = Principal.Identity.Name,
                ModifiedBy = Principal.Identity.Name,
                Version = 1
            };

            var history = RecipeHistoryRepository.Create(recipeHistory);
            output.Output.FullTextReference = history.ID;

            return output;
        }

        private async Task PopulateSteps(Recipe recipe)
        {
            recipe.Steps = await StepRepository.GetByRecipeIdAsync(recipe.ID);
        }

        private async Task PopulateIngredients(Recipe recipe)
        {
            recipe.Ingredients = await RecipeIngredientRepository.GetByRecipeIdAsync(recipe.ID);
        }
    }
}