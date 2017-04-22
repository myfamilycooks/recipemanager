using System.Threading.Tasks;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Data
{
    public interface IRecipeRepository : IDataRepository<Recipe>
    {
        Task<Recipe> GetByKey(string key);
    }
}