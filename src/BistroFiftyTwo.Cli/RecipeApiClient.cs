using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks; 

namespace BistroFiftyTwo.Cli
{
    public class Token
    {
        public string token { get; set; }
    }
    public class RecipeApiClient
    {
        public CommandArguments Arguments { get; set; }
        public string Token { get; set; }

        public async Task Login()
        {
            var client = new HttpClient();
           // client.BaseAddress = new Uri(Arguments.Host);
            var response = await client.PostAsJsonAsync(new Uri($"{Arguments.Host}/token"), new { username = Arguments.Username, password = Arguments.Password});
            var token = await response.Content.ReadAsAsync<Token>();

            Token = token.token;
        }

        public async Task<ParsedRecipe> ParseRecipe(string recipeContents)
        {
            var client = new HttpClient();
            // client.BaseAddress = new Uri(Arguments.Host);
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {Token}");
            var response = await client.PostAsync(new Uri($"{Arguments.Host}/api/parser/standard"), new StringContent(recipeContents));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ParsedRecipe>();
        }

        public async Task<String> CreateRecipe(Recipe createRecipe)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {Token}");
            var response = await client.PostAsync<Recipe>(new Uri($"{Arguments.Host}/api/recipe"), createRecipe, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public class Error
        {
            public int line { get; set; }
            public int errorType { get; set; }
            public int character { get; set; }
            public string unparsedLine { get; set; }
            public int errorCode { get; set; }
            public string description { get; set; }
        }
        public class Ingredient
        {
            public int ordinal { get; set; }
            public string recipeId { get; set; }
            public double quantity { get; set; }
            public string units { get; set; }
            public string ingredient { get; set; }
            public string notes { get; set; }
            public string id { get; set; }
            public DateTime createdDate { get; set; }
            public object createdBy { get; set; }
            public DateTime modifiedDate { get; set; }
            public object modifiedBy { get; set; }
        }

        public class Step
        {
            public int ordinal { get; set; }
            public string recipeId { get; set; }
            public string instructions { get; set; }
            public string id { get; set; }
            public DateTime createdDate { get; set; }
            public object createdBy { get; set; }
            public DateTime modifiedDate { get; set; }
            public object modifiedBy { get; set; }
        }

        public class Recipe
        {
            public string title { get; set; }
            public string key { get; set; }
            public string tags { get; set; }
            public string description { get; set; }
            public object notes { get; set; }
            public List<Ingredient> ingredients { get; set; }
            public List<Step> steps { get; set; }
            public string fullTextReference { get; set; }
            public string id { get; set; }
            public DateTime createdDate { get; set; }
            public object createdBy { get; set; }
            public DateTime modifiedDate { get; set; }
            public object modifiedBy { get; set; }
        }

        public class ParsedRecipe
        {
            public List<Error> errors { get; set; }
            public int status { get; set; }
            public object input { get; set; }
            public Recipe output { get; set; }
        }
    }
}
