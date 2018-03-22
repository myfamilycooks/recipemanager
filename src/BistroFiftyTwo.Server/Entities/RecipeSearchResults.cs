using System.Collections.Generic;
using System.Linq;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeSearchResults
    {
        public IEnumerable<RecipeSearchResult> Recipes { get; set; }
        public int Skip { get; set; } = 0;
        public int Count => Enumerable.Count<RecipeSearchResult>(Recipes);
        public int Take => Enumerable.Count<RecipeSearchResult>(Recipes);

    }
}