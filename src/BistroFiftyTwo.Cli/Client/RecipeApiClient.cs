using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Cli.Client
{
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

       
    }
}
