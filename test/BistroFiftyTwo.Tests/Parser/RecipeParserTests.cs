using BistroFiftyTwo.Server.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Tests.Parser
{
    public class RecipeParserTests
    {
        [Fact, Trait("Parser Tests", "When parsing an empty recipe")]
        public void Parser_Received_Empty_String()
        {
            // Arrange
            var inputString = "";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
            var parseError = new ParseError()
            {
                Character = -1,
                Line = -1,
                Description = "Empty String was provided.  No Content to Parse",
                ErrorCode = ParseErrorCode.NoInput,
                ErrorType = ErrorType.Fatal,
                UnparsedLine = ""
            };

            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.Failed, output.Status);
            Assert.NotEmpty(output.Errors);
            Assert.Single<ParseError>(output.Errors);
            Assert.Matches(parseError.ToString(), output.Errors.FirstOrDefault().ToString());
        }

        [Fact,  Trait("Parser Tests", "When Parsing a recipe with no ingredients")]
        public void Parser_Received_Recipe_With_No_Ingredients()
        {
            // Arrange
            var inputString = @"
Puppies
Kittens
Bunnies

";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
            var parseError = new ParseError()
            {
                Character = -1,
                Line = -1,
                Description = "Could Not find any ingredients",
                ErrorCode = ParseErrorCode.NoIngredients,
                ErrorType = ErrorType.MissingSection,
                UnparsedLine = ""
            };

            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.ParsedWithErrors, output.Status);
            Assert.NotEmpty(output.Errors); 
            Assert.Matches(parseError.ToString(), output.Errors.Single(c => c.ErrorCode == ParseErrorCode.NoIngredients).ToString());
        }

        [Fact, Trait("Parser Tests", "When Parsing a recipe with proper sections")]
        public void Parser_Received_Recipe_With_Proper_Sections()
        {
            // Arrange
            var inputString = @"
description

my description

ingredients

my ingredients

instructions

my instructions

";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
           
            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.Succeeded, output.Status);
            Assert.Empty(output.Errors); 
        }

        [Fact, Trait("Parser Tests", "When Parsing a recipe with no instructions")]
        public void Parser_Received_Recipe_With_No_Instructions()
        {
            // Arrange
            var inputString = @"
Puppies
Kittens
Bunnies

";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
            var parseError = new ParseError()
            {
                Character = -1,
                Line = -1,
                Description = "Could Not find any instructions",
                ErrorCode = ParseErrorCode.NoInstructions,
                ErrorType = ErrorType.MissingSection,
                UnparsedLine = ""
            };

            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.ParsedWithErrors, output.Status);
            Assert.NotEmpty(output.Errors);
            Assert.Matches(parseError.ToString(), output.Errors.Single(c => c.ErrorCode == ParseErrorCode.NoInstructions).ToString());
        }

        [Fact, Trait("Parser Tests", "When Parsing a recipe with no description")]
        public void Parser_Received_Recipe_With_No_Description()
        {
            // Arrange
            var inputString = @"
Puppies
Kittens
Bunnies

";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
            var parseError = new ParseError()
            {
                Character = -1,
                Line = -1,
                Description = "Could Not find any description",
                ErrorCode = ParseErrorCode.NoDescription,
                ErrorType = ErrorType.MissingSection,
                UnparsedLine = ""
            };

            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.ParsedWithErrors, output.Status);
            Assert.NotEmpty(output.Errors);
            Assert.Matches(parseError.ToString(), output.Errors.Single(c => c.ErrorCode == ParseErrorCode.NoDescription).ToString());
        }

        [Fact, Trait("Parser Tests", "When Parsing a full recipe")]
        public void Parser_Received_Full_Recipe()
        {
            // Arrange
            var inputString = @"
description

a great recipe for chicken wings

ingredients

1 cup franks red hot
1 cup butter
1 cup bbq sauce
1 cup v8
1 lb chicken wings

instructions

1) mix redhot, butter, bbq sauce, and v8 together stirring frequently
2) bake, grill or deep fry chicken wings
3) coat chicken wings in sauce 
4) put wings on grill for a minute or two (optional)

";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var recipeParser = new RecipeParser(recipeParserConfiguration);
            var recipe = new Recipe()
            {
                Description = "a great recipe for chicken wings",
                Steps = new List<Step>()
                {
                    new Step() { Ordinal = 1, Instructions = "mix redhot, butter, bbq sauce, and v8 together stirring frequently"},
                    new Step() { Ordinal = 2, Instructions = "bake, grill or deep fry chicken wings"},
                    new Step() { Ordinal = 3, Instructions = "coat chicken wings in sauce"},
                    new Step() { Ordinal = 4, Instructions = "put wings on grill for a minute or two (optional)"},
                }
            };

            // Act 
            var output = recipeParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ParseStatus.Succeeded, output.Status);
            Assert.Empty(output.Errors);
            Assert.NotEmpty(output.Output.Ingredients);
            Assert.NotEmpty(output.Output.Steps);
            Assert.Matches(recipe.Description, output.Output.Description);
            Assert.Equal(recipe.Steps.Single(s => s.Ordinal == 1).Instructions, output.Output.Steps.Single(s => s.Ordinal == 1).Instructions);
            Assert.Equal(recipe.Steps.Single(s => s.Ordinal == 2).Instructions, output.Output.Steps.Single(s => s.Ordinal == 2).Instructions);
            Assert.Equal(recipe.Steps.Single(s => s.Ordinal == 3).Instructions, output.Output.Steps.Single(s => s.Ordinal == 3).Instructions);
            Assert.Equal(recipe.Steps.Single(s => s.Ordinal == 4).Instructions, output.Output.Steps.Single(s => s.Ordinal == 4).Instructions);

        }
    }
}
