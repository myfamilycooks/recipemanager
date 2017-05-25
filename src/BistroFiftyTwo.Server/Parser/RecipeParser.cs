using BistroFiftyTwo.Server.Parser.Scanner;
using System;
using System.Linq;
using System.Text;
 

namespace BistroFiftyTwo.Server.Parser
{
    public class RecipeParser : IRecipeParser
    {
        protected ParserConfiguration Configuration { get; set; }
        protected IngredientParser IngredientParser { get; set; }

        public RecipeParser(ParserConfiguration config)
        {
            Configuration = config;
            IngredientParser = new IngredientParser(Configuration);
        }
        public ParserResult Parse(string input)
        {
            var result = new ParserResult();

            if(String.IsNullOrEmpty(input))
            {
                result.Errors.Add(new ParseError()
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

                if(!ValidateScannedRecipe(scannedRecipe, result))
                {
                    result.Status = ParseStatus.ParsedWithErrors;
                }
                else
                {
                    ParseIngredients(scannedRecipe, result);
                }
            }
            return result;
        }

        private void ParseIngredients(ScannedRecipe scannedRecipe, ParserResult result)
        {
            scannedRecipe.IngredientSection.Content.ForEach(c =>
            {
                if (!String.IsNullOrEmpty(c))
                {
                    var ingredient = IngredientParser.Parse(c);

                    result.Output.Ingredients.Append(ingredient);
                }
            });
        }

        private bool ValidateScannedRecipe(ScannedRecipe scannedRecipe, ParserResult result)
        {
            if (!scannedRecipe.IngredientSection.Content.Any())
            {
                result.Errors.Add(new ParseError()
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any ingredients",
                    ErrorCode = ParseErrorCode.NoIngredients,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });
            }

            if (!scannedRecipe.DescriptionSection.Content.Any())
            {
                result.Errors.Add(new ParseError()
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any description",
                    ErrorCode = ParseErrorCode.NoDescription,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });
            }

            if (!scannedRecipe.InstructionSection.Content.Any())
            {
                result.Errors.Add(new ParseError()
                {
                    Character = -1,
                    Line = -1,
                    Description = "Could Not find any instructions",
                    ErrorCode = ParseErrorCode.NoInstructions,
                    ErrorType = ErrorType.MissingSection,
                    UnparsedLine = ""
                });
            }

            if (result.Errors.Any(e => e.ErrorType == ErrorType.MissingSection))
                return false;
            else
                return true;
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
            catch(Exception ex)
            {
                result.Status = ParseStatus.Failed;

                if(Configuration.ReportExceptions)
                {
                    result.Errors?.Add(new ParseError() { Character = -1, Line = -1, Description = $"Exception Occurred - {ex.Message}", ErrorCode = ParseErrorCode.ExceptionOccurred, ErrorType = ErrorType.Fatal, UnparsedLine = "" });
                }
            }

            if (result.Status == default(ParseStatus))
            {
                result.Status = ParseStatus.Failed;
                result.Errors?.Add(new ParseError() { Character = -1, Line = -1, Description = "Unknown Error", ErrorCode = ParseErrorCode.UnknownError, ErrorType = ErrorType.Fatal, UnparsedLine = "" });
                return false;
            }

            return false;
        }
    }
}
