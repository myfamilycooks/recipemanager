using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeQuery
    {
        public string SearchText { get; set; }
    }

    public class RecipeSearchResults
    {
        public IEnumerable<RecipeSearchResult> Recipes { get; set; }
        public int Skip { get; set; } = 0;
        public int Count => Recipes.Count();
        public int Take => Recipes.Count();

    }
    public class RecipeSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
