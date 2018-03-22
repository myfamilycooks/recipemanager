using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RecipeHistoryRepository : BaseRepository, IRecipeHistoryRepository
    {
        public RecipeHistoryRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public async Task<RecipeHistory> GetAsync(Guid id)
        {
            var query =
                "select id, recipeid, version, fulltext, createddate, createdby, modifieddate, modifiedby from recipe_histories where id = @id";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeHistory>(query, new {id});
            }
        }

        public async Task<RecipeHistory> CreateAsync(RecipeHistory item)
        {
            var query =
                @"insert into recipe_histories (recipeid, version, fulltext, document,  createdby, modifiedby) values (@recipeid, @version, @fulltext, to_tsvector(@fulltext), @createdby, @modifiedby) returning *";

            var record = new
            {
                recipeid = item.RecipeID,
                version = item.Version,
                fulltext = item.FullText,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeHistory>(query, record);
            }
        }

        public async Task<IEnumerable<RecipeHistory>> GetAllAsync()
        {
            var query =
                "select id, recipeid, version, fulltext, createddate, createdby, modifieddate, modifiedby from recipe_histories  ";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<RecipeHistory>(query);
            }
        }

        public async Task<RecipeHistory> UpdateAsync(RecipeHistory item)
        {
            var query =
                @"update recipe_histories set recipeid = @recipeid,  modifiedby = @modifiedby, modifieddate = now() where id = @id returning *";

            var record = new
            {
                id = item.ID,
                recipeid = item.RecipeID,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RecipeHistory>(query, record);
            }
        }

        public async Task DeleteAsync(RecipeHistory item)
        {
            var query = "delete from recipe_histories where id = @id";

            using (var connection = await CreateConnection())
            {
                await connection.ExecuteAsync(query, new {id = item.ID});
            }
        }

        public async Task<IEnumerable<RecipeHistory>> GetHistoryForRecipe(Guid recipeId)
        {
            var query =
                "select id, recipeid, version, fulltext, createddate, createdby, modifieddate, modifiedby from recipe_histories where recipeid = @recipeId order by version;";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<RecipeHistory>(query, new {recipeId});
            }
        }

        public RecipeHistory Create(RecipeHistory item)
        {
            var query =
                @"insert into recipe_histories (version, fulltext,  createdby,  modifiedby) values (  @version, @fulltext,  @createdby,   @modifiedby) returning *";

            var record = new
            {
                version = item.Version,
                fulltext = item.FullText,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = CreateConnectionSynchronous())
            {
                return connection.QuerySingle<RecipeHistory>(query, record);
            }
        }

        public async Task UpdateSearchIndex(Guid id)
        {
            var query = "update recipe_histories set document = to_tsvector(fulltext) where id = @id";

            using (var connection = await CreateConnection())
            {
                await connection.ExecuteAsync(query, new {id});
            }
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

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

        #endregion
    }
}