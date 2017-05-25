﻿using BistroFiftyTwo.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace BistroFiftyTwo.Server.Parser
{
    public class IngredientParser
    {
        protected ParserConfiguration Configuration { get; set; }
        public IngredientParser(ParserConfiguration config) {
            Configuration = config;
        }

        private static Regex QuantityRegex = new Regex(@"[0-9]+(\/[0-9]+)?", RegexOptions.Compiled);

        public RecipeIngredient Parse(string ingredientText)
        {
            var recipeIngredient = new RecipeIngredient();
            /*
             *  1 cup diced onions
            */
            // drop the noise words.
            Configuration.NoiseWords.ForEach(n => ingredientText = ingredientText.Replace(n, ""));

            var ingredientTokens = Lexer.Tokenize(ingredientText);

            recipeIngredient = ParseQuantity(recipeIngredient,ref ingredientTokens);
            recipeIngredient = ParseUnits(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseItemQualifiers(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseItem(recipeIngredient, ref ingredientTokens);
            recipeIngredient = ParseNotes(recipeIngredient, ref ingredientTokens);

            return recipeIngredient;
        }

        private RecipeIngredient ParseItemQualifiers(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var ingredientItem = new StringBuilder();

            while (ingredientTokens != null && !String.IsNullOrEmpty(ingredientTokens.Value) && Configuration.IngredientQualifiers.Contains(ingredientTokens.Value))
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

            while (ingredientTokens != null && !String.IsNullOrEmpty(ingredientTokens.Value))
            {
                if(!ingredientTokens.Value.Equals("--"))
                {
                    notesItem.Append($"{ingredientTokens.Value} ");
                }

                ingredientTokens = ingredientTokens.Next;
            }

            if(!String.IsNullOrEmpty(notesItem.ToString()))
                recipeIngredient.Notes = String.IsNullOrEmpty(recipeIngredient.Notes) ? notesItem.ToString().Trim() : $"{recipeIngredient.Notes}, {notesItem.ToString().Trim()}";
            return recipeIngredient;
        }

        private RecipeIngredient ParseItem(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            var ingredientItem = new StringBuilder();

            while (ingredientTokens != null && !String.IsNullOrEmpty(ingredientTokens.Value) && !ingredientTokens.Value.Equals("--"))
            {
                ingredientItem.Append($"{ingredientTokens.Value} ");
                ingredientTokens = ingredientTokens.Next;
            }

            recipeIngredient.Ingredient = ingredientItem.ToString().Trim();
            return recipeIngredient;
        }

        private RecipeIngredient ParseUnits(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            foreach(var unit in Configuration.MeasurementUnits)
            {
                if (unit.Value.Contains(ingredientTokens.Value))
                {
                    recipeIngredient.Units = ingredientTokens.Value;
                    ingredientTokens = ingredientTokens.Next;
                    break;
                }
            }

            return recipeIngredient;
        }

        private RecipeIngredient ParseQuantity(RecipeIngredient recipeIngredient, ref Token ingredientTokens)
        {
            if (QuantityRegex.IsMatch(ingredientTokens.Value))
            {
                if(ingredientTokens.Next != null && QuantityRegex.IsMatch(ingredientTokens.Next.Value))
                {
                    double fractionalValue = ParseFraction(ingredientTokens.Next.Value);

                    var otherValue = Double.Parse(ingredientTokens.Value);

                    recipeIngredient.Quantity = otherValue + fractionalValue;

                    ingredientTokens = ingredientTokens.Next.Next;
                }
                else
                {
                    if (ingredientTokens.Value.Contains('/'))
                    {
                        recipeIngredient.Quantity = ParseFraction(ingredientTokens.Value);
                    }
                    else
                        recipeIngredient.Quantity = Double.Parse(ingredientTokens.Value);

                    ingredientTokens = ingredientTokens.Next;
                }
            }

            return recipeIngredient;
        }

        private double ParseFraction(string fraction)
        {
            var splitFraction = fraction.Split('/');
            var numerator = Double.Parse(splitFraction.First());
            var denominator = Double.Parse(splitFraction.Last());
            var fractionalValue = numerator / denominator;
            return fractionalValue;
        }
    }

    public class Token
    {
        public string Value { get; set; }
        public Token Next { get; set; }
        public Token Previous { get; set; }
    }

    public class Lexer
    {
        public static Token Tokenize(string input)
        {
            var parts = Regex.Split(input, @"\s+");
            var tokens = new List<string>();
            var token = new Token();

            if(parts != null && parts.Any())
            {
                tokens = parts.ToList();
            }

            tokens.ForEach(t =>
            {
                if (!String.IsNullOrEmpty(t))
                {
                    var newToken = new Token()
                    {
                        Previous = token
                    };

                    token.Value = t;
                    token.Next = newToken;
                    token = token.Next;
                }
            });

            while (token.Previous != null)
                token = token.Previous;

            return token;
        }
    }
}
