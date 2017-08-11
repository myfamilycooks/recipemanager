using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace BistroFiftyTwo.Server.Repositories
{

    public class RecipeIngredientRepository : AutomaticDataRepository<RecipeIngredient>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(IConfigurationService configurationService) : base(configurationService)
        {

        }
 
        private bool disposedValue = false; // To detect redundant calls

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

        public async Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId)
        {
            return await Connection.QueryAsync<RecipeIngredient>("select * from recipe_ingredients where recipeid = @recipeId", new { recipeId });
        }
    }
}
