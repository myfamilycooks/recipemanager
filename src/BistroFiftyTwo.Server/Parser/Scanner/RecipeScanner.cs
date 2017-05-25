using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BistroFiftyTwo.Server.Parser.Scanner
{
    public class ScannedRecipe
    {
        public ScannedRecipe()
        {
            DescriptionSection = new RecipeSection();
            IngredientSection = new RecipeSection();
            InstructionSection = new RecipeSection();
        }

        public RecipeSection DescriptionSection { get; set; }
        public RecipeSection IngredientSection { get; set; }
        public RecipeSection InstructionSection { get; set; }
    }
    public class RecipeSection
    {
        public RecipeSection()
        {
            Content = new List<string>();
        }
        public string SectionName { get; set; }
        public List<string> Content { get; set; }
    }
    public class RecipeScanner
    {
        protected ParserConfiguration Configuration { get; set; }

        public RecipeScanner(ParserConfiguration config)
        {
            Configuration = config;
        }

        public ScannedRecipe Scan(string recipe)
        {
            var scanned = new ScannedRecipe();

            var lines = recipe.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

            var currentSection = new RecipeSection();

            lines.ForEach(s =>
            {
                switch (s.Trim().ToLower()) {
                    case "description":
                        CloseSection(scanned, "description", currentSection);
                        break;
                    case "instructions":
                        CloseSection(scanned, "instructions", currentSection);
                        break;
                    case "ingredients":
                        CloseSection(scanned, "ingredients", currentSection);
                        break;
                    default:
                        currentSection.Content.Add(s);
                        break;
                }
            });

            // close the last section...
            CloseSection(scanned, "", currentSection);

            return scanned;
        }

        private void CloseSection(ScannedRecipe scanned, string newSectionName, RecipeSection currentSection)
        {
            switch (currentSection.SectionName)
            {
                case "description":
                    scanned.DescriptionSection.Content = currentSection.Content;
                    break;
                case "instructions":
                    scanned.InstructionSection.Content = currentSection.Content;
                    break;
                case "ingredients":
                    scanned.IngredientSection.Content = currentSection.Content;
                    break;
            }

            currentSection = new RecipeSection() { SectionName = newSectionName };
        }
    }
}
