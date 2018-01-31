using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Parser;

namespace BistroFiftyTwo.Server.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetByIdAsync(Guid id);
        Task<Recipe> GetByKeyAsync(string key);
        Task<Recipe> ParseAsync(string input);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe> Parse(string input);
        Task<ParserResult> ParseFull(string input);
    }
}