using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Parser
{
    public class ParserConfiguration
    {
        
        public bool ReportExceptions { get; set; }
        public Dictionary<string, List<string>> MeasurementUnits { get; set; } = new Dictionary<string, List<string>>
        {
            { "teaspoon", new List<string> { "teaspoon", "tsp", "tsp(s)", "teaspoons" } },
            { "tablespoon", new List<string> { "tablespoon", "tbl", "tbs", "tbsp", "tablespoons", "tablespoon(s)", "tbsp(s)" } },
            { "cup", new List<string> { "cup", "c", "cups"} },
            { "pint", new List<string> { "pint", "p" } },
            { "quart", new List<string> { "quart", "q", "qt", "fl qt" } },
            { "gallon", new List<string> { "gallon", "g", "gal" } },
            { "pound", new List<string> { "pound", "lb", "lbs" } },
            { "ounce", new List<string> { "ounce", "oz", "oz(s)", "ozs" } },
            { "slices", new List<string> { "slices", "slice" } }
        };
        public List<string> IngredientQualifiers { get; set; } = new List<string> { "diced", "large", "small", "medium", "finely", "halved" };
        public List<string> NoiseWords { get; set; } = new List<string> { "of" };
    }
}
