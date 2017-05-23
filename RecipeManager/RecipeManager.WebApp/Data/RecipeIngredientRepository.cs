using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RecipeManager.WebApp.Entities;
using RecipeManager.WebApp.Services;

namespace RecipeManager.WebApp.Data
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        protected IDatabase Database { get; set; }

        public RecipeIngredientRepository(IDatabase database)
        {
            Database = database;
        }

        public async Task<RecipeIngredient> Create(RecipeIngredient item)
        {
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<RecipeIngredient>> GetAll()
        {
            await Database.Connect();
            return
                await Database.Connection.QueryAsync<RecipeIngredient>(
                    "select * from recipe_ingredients");
        }

        public async Task<RecipeIngredient> Update(RecipeIngredient item)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipeIngredient> Get(Guid id)
        {
            await Database.Connect();
            return
                await Database.Connection.QueryFirstOrDefaultAsync<RecipeIngredient>(
                    "select * from recipe_ingredients where id = @id", new { id = id });
        }

        public async Task Delete(RecipeIngredient item)
        {
            await Database.Connect();
            await Database.Connection.ExecuteAsync("delete from recipe_ingredients where id = @id", new {id = item.Id});
        }

        public async Task<IEnumerable<RecipeIngredient>> GetByRecipeID(Guid recipeId)
        {
            await Database.Connect();
            return
                await Database.Connection.QueryAsync<RecipeIngredient>(
                    "select * from recipe_ingredients where recipeid = @recipeid", new {recipeid = recipeId});
        }
    }
}
