using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace RecipeManager.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(GoAsync());
            }
            catch (AggregateException ax)
            {
                foreach (var ex in ax.InnerExceptions)
                {
                    Console.WriteLine($"Error - {ex.Message}");
                    Console.WriteLine($"Stack - {ex.StackTrace}");
                }
            }

            Console.ReadLine();
        }

        static async Task GoAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("chef", "mustard", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
    
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5000/api/system/whoami");
            if (!response.IsSuccessStatusCode)
                Console.WriteLine(response.StatusCode);
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
