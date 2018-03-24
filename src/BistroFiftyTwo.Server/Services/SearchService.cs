using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;
using Fclp.Internals.Extensions;

namespace BistroFiftyTwo.Server.Services
{
    public class SearchService : ISearchService
    {
        public SearchService(IRecipeRepository recipeRepository, ICacheService cacheService)
        {
            RecipeRepository = recipeRepository;
            CacheService = cacheService;
        }
        private ICacheService CacheService { get; set; }
        private IRecipeRepository RecipeRepository { get; }

        public async Task<RecipeSearchResults> SearchRecipes(RecipeQuery query)
        {
            var recipes = await RecipeRepository.Search(query.SearchText);
            var results = new RecipeSearchResults
            {
                Recipes = new List<RecipeSearchResult>()
            };

            recipes.ForEach(r =>
            {
                ((List<RecipeSearchResult>) results.Recipes).Add(new RecipeSearchResult
                {
                    Id = r.ID,
                    Description = r.Description,
                    Name = r.Title,
                    Url = $"/api/recipe/{r.Key}"
                });
            });

            return results;
        }

        public async Task<IEnumerable<Suggestion>> Suggestions(string term)
        {
            var suggestions  = await CacheService.GetAsync<IEnumerable<Suggestion>>("SearchService$Suggestions");
            if (suggestions == null)
            {
                suggestions = await RecipeRepository.Suggestions();
                await CacheService.SetAsync("SearchService$Suggestions", suggestions, 1000 * 60 * 60);
            }

            return suggestions.Where(s => s.Term.ToLower().StartsWith(term));
        }
    }
}