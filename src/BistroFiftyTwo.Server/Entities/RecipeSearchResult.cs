using System;
using System.Text;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; } = "https://myfamilycooks.blob.core.windows.net/recipe/ingredients.jpg";
    }
}
