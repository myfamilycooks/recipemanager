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

        public async Task<Recipe> GetRecipeByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetRecipeByKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
