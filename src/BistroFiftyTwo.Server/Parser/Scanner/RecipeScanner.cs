using System;
using System.Linq;

namespace BistroFiftyTwo.Server.Parser.Scanner
{
    public class RecipeScanner
    {
        public RecipeScanner(ParserConfiguration config)
        {
            Configuration = config;
        }

        protected ParserConfiguration Configuration { get; set; }

        public ScannedRecipe Scan(string recipe)
        {
            var scanned = new ScannedRecipe();

            var lines = recipe.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None).ToList();

            if (lines.First() != "description" &&
                lines.First() != "instructions" &&
                lines.First() != "ingredients" &&
                !string.IsNullOrEmpty(lines.First().Trim()))
                scanned.Title = lines.First();
            var currentSection = new RecipeSection();

            lines.ForEach(s =>
            {
                switch (s.Trim().ToLower())
                {
                    case "description":
                        CloseSection(scanned, "description", ref currentSection);
                        break;
                    case "instructions":
                        CloseSection(scanned, "instructions", ref currentSection);
                        break;
                    case "ingredients":
                        CloseSection(scanned, "ingredients", ref currentSection);
                        break;
                    default:
                        if (!string.IsNullOrEmpty(s))
                            currentSection.Content.Add(s);
                        break;
                }
            });

            // close the last section...
            CloseSection(scanned, "", ref currentSection);

            return scanned;
        }

        private void CloseSection(ScannedRecipe scanned, string newSectionName, ref RecipeSection currentSection)
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

            currentSection = new RecipeSection {SectionName = newSectionName};
        }
    }
}