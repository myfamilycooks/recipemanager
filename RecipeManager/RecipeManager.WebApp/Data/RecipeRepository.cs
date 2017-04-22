using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using RecipeManager.WebApp.Data;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Services
{
    public class RecipeRepository : IRecipeRepository
    { 
        private IDatabase Database { get; set; }

        public RecipeRepository(IDatabase database)
        {
            Database = database;
        }
        
        public async Task<Recipe> Create(Recipe item)
        {
            var query = @"insert into recipes 
                        (title, key, tags, description, notes, createdby, modifiedby)
                        values 
                        (@title, @key, @tags, @description, @notes, @createdby, @createdby)
                        returning * ";

            var values = new
            {
                title = item.Title,
                key = item.Key,
                description = item.Description,
                notes = item.Notes,
                createdby = item.CreatedBy
            };
            await Database.Connect();
            return await Database.Connection.QuerySingleOrDefaultAsync<Recipe>(query, values);
        }

        public async Task<Recipe> Update(Recipe item)
        {
            var query =
                @"update recipes set title = @title, key = @key, tags = @tags, description = @description, notes = @notes, modifieddate = @modifieddate,  modifiedby = @modifiedby
                            where id = @id
                          returning *;";

            var values = new
            {
                id = item.Id,
                title = item.Title,
                key = item.Key,
                description = item.Description,
                notes = item.Notes,
                modifieddate = item.ModifiedDate,
                modifiedby = item.ModifiedBy,
            };

            await Database.Connect();
            return await Database.Connection.QuerySingleOrDefaultAsync<Recipe>(query, values);
        }

        public async Task<Recipe> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetByKey(string key)
        {
            await Database.Connect();
            return await Database.Connection.QueryFirstOrDefaultAsync<Recipe>("select * from recipes where key = @key;", new { key = key });
        }
        public async Task Delete(Recipe item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            await Database.Connect();
            return await Database.Connection.QueryAsync<Recipe>("select * from recipes;");
        }
    }
}