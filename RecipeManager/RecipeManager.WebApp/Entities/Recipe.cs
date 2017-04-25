using System.Collections.Generic;

namespace RecipeManager.WebApp.Entities
{
    public class Recipe : DataObject
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public IEnumerable<RecipeIngredient> Ingredients { get; set; }
        public IEnumerable<Step> Steps { get; set; }
    }
}