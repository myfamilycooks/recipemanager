using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public async Task<Recipe> GetAsync(Guid id)
        {
            var query =
                "select id, title, key, tags, description, notes, createddate, modifieddate, modifiedby from recipes where id = @id";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Recipe>(query, new {id});
            }
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

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Recipe>(query, record);
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var query =
                "select id, title, key, tags, description, notes, createddate, modifieddate, modifiedby from recipes";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<Recipe>(query);
            }
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

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Recipe>(query, record);
            }
        }

        public async Task DeleteAsync(Recipe item)
        {
            var query = "delete from recipes where id = @id";

            using (var connection = await CreateConnection())
            {
                await connection.ExecuteAsync(query, new {id = item.ID});
            }
        }

        public async Task<Recipe> GetByKeyAsync(string key)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Recipe>("select * from recipes where key = @key", new {key});
            }
        }

        public async Task<IEnumerable<Recipe>> Search(string query)
        {
            var sqlQuery = @"select * from recipes where id in (
                                SELECT recipeid FROM recipe_histories rh 
                                WHERE rh.recipeid is not null and document @@ to_tsquery(@query)
                            );";
            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<Recipe>(sqlQuery, new {query});
            }
        }

        public async Task<IEnumerable<Suggestion>> Suggestions()
        {
            var sqlQuery = @"select distinct ingredient as term, 'ingredient' as category from recipe_ingredients   union select title as term, 'recipe' as category from recipes;";
            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<Suggestion>(sqlQuery);
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