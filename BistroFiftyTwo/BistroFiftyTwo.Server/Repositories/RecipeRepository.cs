using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Npgsql;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RecipeRepository : AutomaticDataRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(IConfigurationService configurationService) : base(configurationService)
        {

        }
      
        #region IDisposable Support
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

        public async Task<Recipe> GetByKeyAsync(string key)
        {
            return await Connection.QuerySingleAsync<Recipe>("select * from recipes where key = @key", new { key });
        }
        #endregion
    }
}
