using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
            IStepRepository stepRepository, IRecipeHistoryRepository recipeHistoryRepository, ISecurityService securityService)
        {
            RecipeRepository = recipeRepository;
            RecipeIngredientRepository = recipeIngredientRepository;
            StepRepository = stepRepository;
            RecipeHistoryRepository = recipeHistoryRepository;
            RecipeParser = new RecipeParser(new ParserConfiguration());
           
            SecurityService = securityService;
        }

        private IRecipeRepository RecipeRepository { get; }
        private IRecipeIngredientRepository RecipeIngredientRepository { get; }
        private IStepRepository StepRepository { get; }
        private IRecipeHistoryRepository RecipeHistoryRepository { get; }
        private IRecipeParser RecipeParser { get; } 
        private ISecurityService SecurityService { get; set; }

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
                recipe.CreatedBy = await SecurityService.GetCurrentUserName();
                recipe.ModifiedBy = await SecurityService.GetCurrentUserName();
            }

            var existingRecipe = await RecipeRepository.GetByKeyAsync(recipe.Key);
            if(existingRecipe != null && existingRecipe.CreatedBy == recipe.CreatedBy)
                throw new BistroFiftyTwoDuplicateRecipeException("A recipe with that key already exists.  Edit the existing recipe instead."); //TODO: Once we have update, call update instead.

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
                    r.CreatedBy = await SecurityService.GetCurrentUserName();
                    r.ModifiedBy = await SecurityService.GetCurrentUserName();
                }

                await RecipeIngredientRepository.CreateAsync(r);
            });

            recipe.Steps.ToList().ForEach(async s =>
            {
                s.RecipeId = createdRecipe.ID;

                if (string.IsNullOrEmpty(s.CreatedBy) || string.IsNullOrEmpty(s.ModifiedBy))
                {
                    s.CreatedBy = await SecurityService.GetCurrentUserName();
                    s.ModifiedBy = await SecurityService.GetCurrentUserName();
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
            return await  Parse(input); 
        }


        public  async Task<Recipe> Parse(string input)
        {
            var parseOutput = RecipeParser.Parse(input);

            var recipeHistory = new RecipeHistory
            {
                FullText = input,
                CreatedBy = await SecurityService.GetCurrentUserName(),
                ModifiedBy = await SecurityService.GetCurrentUserName(),
                Version = 1
            };

            var history = RecipeHistoryRepository.Create(recipeHistory);
            await RecipeHistoryRepository.UpdateSearchIndex(history.ID);

            parseOutput.Output.FullTextReference = history.ID;

            return parseOutput.Output;
        }

        public async Task<ParserResult> ParseFull(string input)
        {
            var output = RecipeParser.Parse(input);
            var recipeHistory = new RecipeHistory
            {
                FullText = input,
                CreatedBy = await SecurityService.GetCurrentUserName(),
                ModifiedBy = await SecurityService.GetCurrentUserName(),
                Version = 1
            };

            var history = RecipeHistoryRepository.Create(recipeHistory);

            await RecipeHistoryRepository.UpdateSearchIndex(history.ID);

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

    public class BistroFiftyTwoDuplicateRecipeException : Exception
    {
        public BistroFiftyTwoDuplicateRecipeException()
        {
        }

        public BistroFiftyTwoDuplicateRecipeException(string message) : base(message)
        {
        }

        public BistroFiftyTwoDuplicateRecipeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BistroFiftyTwoDuplicateRecipeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}