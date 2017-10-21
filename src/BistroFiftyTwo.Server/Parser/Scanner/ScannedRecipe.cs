using System;

namespace BistroFiftyTwo.Server.Parser.Scanner
{
    public class ScannedRecipe
    {
        public ScannedRecipe()
        {
            DescriptionSection = new RecipeSection();
            IngredientSection = new RecipeSection();
            InstructionSection = new RecipeSection();
            Title = $"Untitled-{DateTime.Now.Ticks}";
        }

        public RecipeSection DescriptionSection { get; set; }
        public RecipeSection IngredientSection { get; set; }
        public RecipeSection InstructionSection { get; set; }
        public string Title { get; set; }
    }
}