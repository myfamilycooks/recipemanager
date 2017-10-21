using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Parser.Scanner
{
    public class RecipeSection
    {
        public RecipeSection()
        {
            Content = new List<string>();
        }

        public string SectionName { get; set; }
        public List<string> Content { get; set; }
    }
}