using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISearchService
    {
        Task<RecipeSearchResults> SearchRecipes(RecipeQuery query);
    }
}
