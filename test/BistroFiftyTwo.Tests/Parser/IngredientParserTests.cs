using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BistroFiftyTwo.Tests.Parser
{
    public class IngredientParserTests
    {
        [Fact, Trait("Ingredient Parser","When parsing a properly formed ingredient")]
        public void When_Parsing_a_properly_formed_ingredient()
        {
            // Arrange
            var inputString = "1 cup diced onions";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var ingredientParser = new IngredientParser(recipeParserConfiguration);
            var ingredient = new RecipeIngredient()
            {
                Quantity = 1,
                Units = "cup",
                Ingredient = "onions",
                Notes = "diced"
            };
 
            // Act 
            var output = ingredientParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ingredient.Quantity, output.Quantity);
            Assert.Equal(ingredient.Units, output.Units);
            Assert.Equal(ingredient.Ingredient, output.Ingredient);
            Assert.Equal(ingredient.Notes, output.Notes);

        }
        [Fact, Trait("Ingredient Parser", "When parsing an ingredient with noise words")]
        public void When_Parsing_an_ingredient_with_noise_words()
        {
            // Arrange
            var inputString = "1 cup of diced onions";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var ingredientParser = new IngredientParser(recipeParserConfiguration);
            var ingredient = new RecipeIngredient()
            {
                Quantity = 1,
                Units = "cup",
                Ingredient = "onions",
                Notes = "diced"
            };

            // Act 
            var output = ingredientParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ingredient.Quantity, output.Quantity);
            Assert.Equal(ingredient.Units, output.Units);
            Assert.Equal(ingredient.Ingredient, output.Ingredient);
            Assert.Equal(ingredient.Notes, output.Notes);

        }

        [Fact, Trait("Ingredient Parser", "When parsing an ingredient with a fractional quantity")]
        public void When_Parsing_an_ingredient_with_a_fractional_quantity()
        {
            // Arrange
            var inputString = "1/2 cup diced onions";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var ingredientParser = new IngredientParser(recipeParserConfiguration);
            var ingredient = new RecipeIngredient()
            {
                Quantity = .5,
                Units = "cup",
                Ingredient = "onions",
                Notes = "diced"
            };

            // Act 
            var output = ingredientParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ingredient.Quantity, output.Quantity);
            Assert.Equal(ingredient.Units, output.Units);
            Assert.Equal(ingredient.Ingredient, output.Ingredient);
            Assert.Equal(ingredient.Notes, output.Notes);

        }

        [Fact, Trait("Ingredient Parser", "When parsing an ingredient with a complex fractional quantity")]
        public void When_Parsing_an_ingredient_with_a_complex_fractional_quantity()
        {
            // Arrange
            var inputString = "3 1/2 cup diced onions";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var ingredientParser = new IngredientParser(recipeParserConfiguration);
            var ingredient = new RecipeIngredient()
            {
                Quantity = 3.5,
                Units = "cup",
                Ingredient = "onions",
                Notes = "diced"
            };

            // Act 
            var output = ingredientParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ingredient.Quantity, output.Quantity);
            Assert.Equal(ingredient.Units, output.Units);
            Assert.Equal(ingredient.Ingredient, output.Ingredient);
            Assert.Equal(ingredient.Notes, output.Notes);

        }

        [Fact, Trait("Ingredient Parser", "When parsing an ingredient with a complex fractional quantity and complex notes")]
        public void When_Parsing_an_ingredient_with_a_complex_fractional_quantity_and_complex_notes()
        {
            // Arrange
            var inputString = "3 1/2 cup diced onions -- use yellow onions";
            var recipeParserConfiguration = new ParserConfiguration() { ReportExceptions = true };
            var ingredientParser = new IngredientParser(recipeParserConfiguration);
            var ingredient = new RecipeIngredient()
            {
                Quantity = 3.5,
                Units = "cup",
                Ingredient = "onions",
                Notes = "diced, use yellow onions"
            };

            // Act 
            var output = ingredientParser.Parse(inputString);

            // Assert 
            Assert.NotNull(output);
            Assert.Equal(ingredient.Quantity, output.Quantity);
            Assert.Equal(ingredient.Units, output.Units);
            Assert.Equal(ingredient.Ingredient, output.Ingredient);
            Assert.Equal(ingredient.Notes, output.Notes);

        }
    }
}
