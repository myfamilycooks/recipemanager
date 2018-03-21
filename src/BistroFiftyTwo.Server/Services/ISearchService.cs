using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser;
using BistroFiftyTwo.Server.Repositories;
using Fclp.Internals.Extensions;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISearchService
    {
        Task<RecipeSearchResults> SearchRecipes(RecipeQuery query);
    }

    public class SearchService : ISearchService
    {
        public SearchService(IRecipeRepository recipeRepository)
        {
            RecipeRepository = recipeRepository;
        }

        private IRecipeRepository RecipeRepository { get; }
       
        public async Task<RecipeSearchResults> SearchRecipes(RecipeQuery query)
        {
            var recipes = await RecipeRepository.Search(query.SearchText);
            var results = new RecipeSearchResults()
            {
                Recipes = new List<RecipeSearchResult>()
            };

            recipes.ForEach(r =>
            {
                ((List<RecipeSearchResult>)results.Recipes).Add(new RecipeSearchResult()
                {
                    Id = r.ID,
                    Description = r.Description,
                    Name =  r.Title,
                    Url = $"/api/recipe/{r.Key}"
                });
            });

            return results;
        }
    }
}
