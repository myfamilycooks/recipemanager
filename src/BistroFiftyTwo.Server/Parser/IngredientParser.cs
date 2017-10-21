using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Parser
{
    public class IngredientParser
    {
        private static readonly Regex QuantityRegex = new Regex(@"[0-9]+(\/[0-9]+)?", RegexOptions.Compiled);

        public IngredientParser(ParserConfiguration config)
        {
            Configuration = config;
        }

        protected ParserConfiguration Configuration { get; set; }

        public RecipeIngredient Parse(string ingredientText)
        {
            var recipeIngredient = new RecipeIngredient();
            /*
             *  1 cup diced onions
            */
            // drop the noise words.
            Configuration.NoiseWords.ForEach(n => ingredientText = ingredientText.Replace(n, ""));

            var ingredientTokens = Lexer.Tokenize(ingredientText);

            recipeIngredient = ParseQuantity(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseUnits(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseItemQualifiers(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseItem(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseNotes(recipeIngredient, ref ingredientTokens);

            return recipeIngredient;
        }

        private RecipeIngredient ParseItemQualifiers(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var ingredientItem = new StringBuilder();

            while (ingredientTokens != null && !string.IsNullOrEmpty(ingredientTokens.Value) &&
                   Configuration.IngredientQualifiers.Contains(ingredientTokens.Value))
            {
                ingredientItem.Append($"{ingredientTokens.Value} ");
                ingredientTokens = ingredientTokens.Next;
            }

            recipeIngredient.Notes = ingredientItem.ToString().Trim();
            return recipeIngredient;
        }

        private RecipeIngredient ParseNotes(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var notesItem = new StringBuilder();

            while (ingredientTokens != null && !string.IsNullOrEmpty(ingredientTokens.Value))
            {
                if (!ingredientTokens.Value.Equals("--"))
                    notesItem.Append($"{ingredientTokens.Value} ");

                ingredientTokens = ingredientTokens.Next;
            }

            if (!string.IsNullOrEmpty(notesItem.ToString()))
                recipeIngredient.Notes = string.IsNullOrEmpty(recipeIngredient.Notes)
                    ? notesItem.ToString().Trim()
                    : $"{recipeIngredient.Notes}, {notesItem.ToString().Trim()}";
            return recipeIngredient;
        }

        private RecipeIngredient ParseItem(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var ingredientItem = new StringBuilder();

            while (ingredientTokens != null && !string.IsNullOrEmpty(ingredientTokens.Value) &&
                   !ingredientTokens.Value.Equals("--"))
            {
                ingredientItem.Append($"{ingredientTokens.Value} ");
                ingredientTokens = ingredientTokens.Next;
            }

            recipeIngredient.Ingredient = ingredientItem.ToString().Trim();
            return recipeIngredient;
        }

        private RecipeIngredient ParseUnits(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var lowerToken = ingredientTokens.Value.ToLower();
            foreach (var unit in Configuration.MeasurementUnits)
                if (unit.Value.Contains(lowerToken))
                {
                    recipeIngredient.Units = unit.Key;
                    ingredientTokens = ingredientTokens.Next;
                    break;
                }

            return recipeIngredient;
        }

        private RecipeIngredient ParseQuantity(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            if (QuantityRegex.IsMatch(ingredientTokens.Value))
                if (ingredientTokens.Next != null && QuantityRegex.IsMatch(ingredientTokens.Next.Value))
                {
                    var fractionalValue = ParseFraction(ingredientTokens.Next.Value);

                    var otherValue = double.Parse(ingredientTokens.Value);

                    recipeIngredient.Quantity = otherValue + fractionalValue;

                    ingredientTokens = ingredientTokens.Next.Next;
                }
                else
                {
                    if (ingredientTokens.Value.Contains('/'))
                        recipeIngredient.Quantity = ParseFraction(ingredientTokens.Value);
                    else
                        recipeIngredient.Quantity = double.Parse(ingredientTokens.Value);

                    ingredientTokens = ingredientTokens.Next;
                }

            return recipeIngredient;
        }

        private double ParseFraction(string fraction)
        {
            var splitFraction = fraction.Split('/');
            var numerator = double.Parse(splitFraction.First());
            var denominator = double.Parse(splitFraction.Last());
            var fractionalValue = numerator / denominator;
            return fractionalValue;
        }
    }
}