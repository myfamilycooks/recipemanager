using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Data
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        public async Task<RecipeIngredient> Create(RecipeIngredient item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<RecipeIngredient> Update(RecipeIngredient item)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipeIngredient> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(RecipeIngredient item)
        {
            throw new NotImplementedException();
        }
    }
}
