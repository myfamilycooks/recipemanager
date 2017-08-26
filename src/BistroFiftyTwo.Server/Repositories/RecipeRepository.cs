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
    public class RecipeRepository : IRecipeRepository
    {

        protected NpgsqlConnection Connection { get; set; }
        public RecipeRepository(IConfigurationService configurationService)  
        {
            Connection = new NpgsqlConnection(configurationService.Get("Data:Recipe:ConnectionString"));
            Connection.Open();
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

        public async Task<Recipe> GetAsync(Guid id)
        {
            var query =
                "select id, title, key, tags, description, notes, createddate, modifieddate, modifiedby from recipes where id = @id";

            return await Connection.QuerySingleAsync<Recipe>(query, id);
        }

        public async Task<Recipe> CreateAsync(Recipe item)
        {
            var query =
                @"insert into recipes (title, key, tags, description, notes, createdby, modifiedby) values (@title, @key, @tags, @description, @notes, @createdby,@modifiedby) returning *";
           
            var record = new
            {
                title = item.Title,
                key = item.Key,
                tags = item.Tags,
                description = item.Description,
                notes = item.Notes,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            return await Connection.QuerySingleAsync<Recipe>(query, record);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var query =
                "select id, title, key, tags, description, notes, createddate, modifieddate, modifiedby from recipes";

            return await Connection.QueryAsync<Recipe>(query);
        }

        public async Task<Recipe> UpdateAsync(Recipe item)
        {
            var query =
                @"update recipes set title = @title, key = @key, tags = @tags, description = @description, notes = @notes, modifiedby = @modifiedby, modifieddate = now() where id = @id returning *";

            var record = new
            {
                id = item.ID,
                title = item.Title,
                key = item.Key,
                tags = item.Tags,
                description = item.Description,
                notes = item.Notes,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            return await Connection.QuerySingleAsync<Recipe>(query, record);
        }

        public async Task DeleteAsync(Recipe item)
        {
            var query = "delete from recipes where id = @id";

            await Connection.ExecuteAsync(query, new {id = item.ID});
        }
    }
}
