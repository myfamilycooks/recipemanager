using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RecipeIngredientRepository : BaseRepository, IRecipeIngredientRepository
    {
        private bool disposedValue; // To detect redundant calls

        public RecipeIngredientRepository(IConfigurationService configurationService) 
        {
            ConfigurationService = configurationService;
        }

        public async Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<RecipeIngredient>(
                    "select * from recipe_ingredients where recipeid = @recipeId", new {recipeId});
            }
        }

        public async Task<RecipeIngredient> GetAsync(Guid id)
        {
            var query =
                "select id, recipeid, ordinal, quantity, units, item, notes, createddate, createdby, modifieddate, modifiedby from recipe_ingredients where id = @id";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeIngredient>(query, new {id});
            }
        }

        public async Task<RecipeIngredient> CreateAsync(RecipeIngredient item)
        {
            var query =
                "insert into recipe_ingredients (recipeid, ordinal, quantity, units, ingredient, notes, createdby, modifiedby) values (@recipeid, @ordinal, @quantity, @units, @ingredient, @notes, @createdby, @modifiedby) returning *";

            var record = new
            {
                recipeid = item.RecipeId,
                ordinal = item.Ordinal,
                quantity = item.Quantity,
                units = item.Units,
                ingredient = item.Ingredient,
                notes = item.Notes,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeIngredient>(query, record);
            }
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAllAsync()
        {
            var query =
                "select id, recipeid, ordinal, quantity, units, ingredient, notes, createddate, createdby, modifieddate, modifiedby from recipe_ingredients";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<RecipeIngredient>(query);
            }
        }

        public async Task<RecipeIngredient> UpdateAsync(RecipeIngredient item)
        {
            var query =
                "update recipe_ingredients set ordinal = @ordinal, quantity = @quantity, units = @units, item = @item, notes = @notes, modfiedby = @modifiedby, modifieddate = now() where id = @id returning *";

            var record = new
            {
                id = item.ID,
                recipeid = item.RecipeId,
                ordinal = item.Ordinal,
                quantity = item.Quantity,
                units = item.Units,
                ingredient = item.Ingredient,
                notes = item.Notes,
                modifiedby = item.ModifiedBy
            };
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeIngredient>(query, record);
            }
        }

        public async Task DeleteAsync(RecipeIngredient item)
        {
            var query = "delete from recipe_ingredients where id = @id";

            using (var connection = await CreateConnection())
            {
                await connection.QueryAsync(query);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RecipeRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}