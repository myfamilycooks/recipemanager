using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser.Scanner;

namespace BistroFiftyTwo.Server.Parser
{
    public class RecipeParser : IRecipeParser
    {
        public RecipeParser(ParserConfiguration config)
        {
            Configuration = config;
            IngredientParser = new IngredientParser(Configuration);
        }

        protected ParserConfiguration Configuration { get; set; }
        protected IngredientParser IngredientParser { get; set; }

        public ParserResult Parse(string input)
        {
            var result = new ParserResult();

            if (string.IsNullOrEmpty(input))
            {
                result.Errors.Add(new ParseError
                {
                    Character = -1,
                    Line = -1,
                    Description = "Empty String was provided.  No Content to Parse",
                    ErrorCode = ParseErrorCode.NoInput,
                    ErrorType = ErrorType.Fatal,
                    UnparsedLine = ""
                });

                result.Status = ParseStatus.Failed;
            }
            else
            {
                // we have something, so lets move deeper.
                var scanner = new RecipeScanner(Configuration);
                var scannedRecipe = scanner.Scan(input);

                result.Output.Title = scannedRecipe.Title.Trim();
                var key = Regex.Replace(result.Output.Title, @"[^\w\s]", "");
                key = Regex.Replace(key, @"\s", "-");
                key = key.ToLower();
                result.Output.Key = key;
                result.Output.Tags = "";

                if (!ValidateScannedRecipe(scannedRecipe, result))
                {
                    result.Status = ParseStatus.ParsedWithErrors;
                }
                else
                {
                    ParseDescription(scannedRecipe, result);
                    ParseIngredients(scannedRecipe, result);
                    ParseInstructions(scannedRecipe, result);
                }
            }
            return result;
        }

        public bool TryParse(string input, out ParserResult result)
        {
            result = new ParserResult();

            try
            {
                result = Parse(input);

                if (result.Status == ParseStatus.Succeeded)
                    return true;
            }
            catch (Exception ex)
            {
                result.Status = ParseStatus.Failed;

                if (Configuration.ReportExceptions)
                    result.Errors?.Add(new ParseError
                    {
                        Character = -1,
                        Line = -1,
                        Description = $"Exception Occurred - {ex.Message}",
                        ErrorCode = ParseErrorCode.ExceptionOccurred,
                        ErrorType = ErrorType.Fatal,
                        UnparsedLine = ""
                    });
            }

            if (result.Status == default(ParseStatus))
            {
                result.Status = ParseStatus.Failed;
                result.Errors?.Add(new ParseError
                {
                    Character = -1,
                    Line = -1,
                    Description = "Unknown Error",
                    ErrorCode = ParseErrorCode.UnknownError,
                    ErrorType = ErrorType.Fatal,
                    UnparsedLine = ""
                });
                return false;
            }

            return false;
        }

        private void ParseDescription(ScannedRecipe scannedRecipe, ParserResult result)
        {
            var description = new StringBuilder();

            scannedRecipe.DescriptionSection.Content.ForEach(c =>
            {
                if (!string.IsNullOrEmpty(c))
                    description.AppendLine(c); // we're appending line because that's how we found it.
            });

            result.Output.Description = description.ToString();
        }

        private void ParseInstructions(ScannedRecipe scannedRecipe, ParserResult result)
        {
            var instructionList = new List<Step>();
            var instructionOrdinal = 1;

            scannedRecipe.InstructionSection.Content.ForEach(c =>
            {
                if (!string.IsNullOrEmpty(c))
                {
                    // get rid of Numbers if they exist at start of line.
                    var correctedStep = Regex.Replace(c.Trim(), @"^\d+(\)|\.)", string.Empty);

                    instructionList.Add(new Step
                    {
                        Ordinal = instructionOrdinal++,
                        Instructions = correctedStep.Trim()
                    });
                }
            });

            result.Output.Steps = instructionList;
        }

        private void ParseIngredients(ScannedRecipe scannedRecipe, ParserResult result)
        {
            var ingredients = new List<RecipeIngredient>();
            var ingredientOrdinal = 1;

            scannedRecipe.IngredientSection.Content.ForEach(c =>
            {
                var correctedIngredient = Regex.Replace(c.Trim(), @"^\d+(\)|\.)", string.Empty);
                if (!string.IsNullOrEmpty(correctedIngredient.Trim()))
                {
                    var ingredient = IngredientParser.Parse(correctedIngredient);
                    ingredient.Ordinal = ingredientOrdinal++;
                    ingredients.Add(ingredient);
                }
            });

            result.Output.Ingredients = ingredients;
        }

        private bool ValidateScannedRecipe(ScannedRecipe scannedRecipe, ParserResult result)
        {
            if (!scannedRecipe.IngredientSection.Content.Any())
                result.Errors.Add(new ParseError
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any ingredients",
                    ErrorCode = ParseErrorCode.NoIngredients,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });

            if (!scannedRecipe.DescriptionSection.Content.Any())
                result.Errors.Add(new ParseError
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any description",
                    ErrorCode = ParseErrorCode.NoDescription,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });

            if (!scannedRecipe.InstructionSection.Content.Any())
                result.Errors.Add(new ParseError
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any instructions",
                    ErrorCode = ParseErrorCode.NoInstructions,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });

            if (result.Errors.Any(e => e.ErrorType == ErrorType.MissingSection))
                return false;
            return true;
        }
    }
}