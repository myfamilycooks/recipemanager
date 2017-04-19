using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RecipeManager.WebApp.Entities;
using Npgsql;
using Dapper;

namespace RecipeManager.WebApp.Services
{
    public interface IDataRepository<T>
    {
        Task<T> Create(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T item);
        Task<T> Get(Guid id);
        Task<T> Get(string key);
        Task Delete(T item);
    }

 
    public interface IDatabase
    {
        NpgsqlConnection Connection { get; set; }
        Task Connect();
    }

    public class Database : IDatabase
    {
        private string ConnectionString { get; set; }

        public Database(string connectionString)
        {
            ConnectionString = connectionString;            
        }

        public async Task Connect()
        {
            if(Connection == null || Connection.State != ConnectionState.Open)
                Connection = new NpgsqlConnection(ConnectionString);

            await Connection.OpenAsync();
        }

        public NpgsqlConnection Connection { get; set; }
    }
    public class RecipeRepository : IDataRepository<Recipe>
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

            return await Database.Connection.QuerySingleOrDefaultAsync<Recipe>(query, values);
        }

        public async Task<Recipe> Update(Recipe item)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> Get(string key)
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
