using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISearchService
    {
        Task<RecipeSearchResults> SearchRecipes(RecipeQuery query);
    }
}