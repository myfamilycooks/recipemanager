using BistroFiftyTwo.Server.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

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
    }
}
