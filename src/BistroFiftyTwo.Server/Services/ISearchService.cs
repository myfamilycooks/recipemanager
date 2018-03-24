using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISearchService
    {
        Task<RecipeSearchResults> SearchRecipes(RecipeQuery query);
        Task<IEnumerable<Suggestion>> Suggestions(string term);
    }
}