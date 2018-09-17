using System;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; } = "https://myfamilycooks.blob.core.windows.net/recipe/ingredients.jpg";
        public string ShortDescription { get; set; }
        public bool Featured { get;  set; }
        public string Key { get; set; }
    }
}