using System.Collections.Generic;
using System.Linq;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeSearchResults
    {
        public IEnumerable<RecipeSearchResult> Recipes { get; set; }
        public int Skip { get; set; } = 0;
        public int Count => Recipes.Count();
        public int Take => Recipes.Count();
    }
}