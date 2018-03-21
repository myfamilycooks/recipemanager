using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Cli.Client;
using Fclp.Internals.Extensions;
using Fclp.Internals.Parsing;

namespace BistroFiftyTwo.Cli.Commands
{
    public class ImportRecipes : ICommand
    {
        public string CommandName { get; set; } = "importrecipes";
        public async Task Execute(CommandArguments arguments)
        {
            var files = Directory.GetFiles(arguments.Folder, arguments.FilePattern);

            var recipeApi = new RecipeApiClient()
            {
                Arguments = arguments
            };

            await recipeApi.Login();

            foreach(var f in files)
            {
                try
                {
                    var recipeText = File.ReadAllText(f);
                    var parsedRecipe = await recipeApi.ParseRecipe(recipeText);
                    var createdRecipe = await recipeApi.CreateRecipe(parsedRecipe.output);

                    Console.WriteLine($"Successfully Created Recipe {Path.GetFileNameWithoutExtension(f)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Parsing & Creating Recipe {f}");
                }
            }
        }
    }
}
