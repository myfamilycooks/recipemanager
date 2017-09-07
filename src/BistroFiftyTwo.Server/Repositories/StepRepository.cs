using BistroFiftyTwo.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using BistroFiftyTwo.Server.Services;

namespace BistroFiftyTwo.Server.Repositories
{
    public class BaseRepository
    {
        protected IConfigurationService ConfigurationService { get; set; }
        protected async Task<NpgsqlConnection> CreateConnection()
        {
            var connection = new NpgsqlConnection(ConfigurationService.Get("Data:Recipe:ConnectionString"));
            await connection.OpenAsync();
            return connection;
        }
    }
    public class StepRepository : BaseRepository, IStepRepository
    {
        public StepRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }
        
        public async Task<Step> CreateAsync(Step item)
        {
            var query =
                "insert into recipe_steps (ordinal, recipeid, instructions, createdby, modifiedby) values (@ordinal, @recipeid, @instructions, @createdby, @modifiedby) returning *";

            var record = new
            {
                ordinal = item.Ordinal,
                recipeid = item.RecipeId,
                instructions = item.Instructions,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Step>(query, record);
            }
        }

        public async Task DeleteAsync(Step item)
        {
            var query = "delete from recipe_steps where id = @id";
             
            using (var connection = await CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id = item.ID });
            }
        }

        public async Task<IEnumerable<Step>> GetAllAsync()
        {
            var query =
                "select id, ordinal, recipeid, instructions, createddate, createdby, modifieddate, modifiedby from recipe_steps";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<Step>(query);
            }
        }

        public async Task<Step> GetAsync(Guid id)
        {
            var query =
                "select id, ordinal, recipeid, instructions, createddate, createdby, modifieddate, modifiedby from recipe_steps id = @id";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Step>(query, new {id});
            }
        }

        public async Task<IEnumerable<Step>> GetByRecipeIdAsync(Guid recipeId)
        { 
            var query = "select * from recipe_steps where recipeid = @recipeId";

            var criteria = new
            {
                recipeId
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<Step>(query, criteria);
            }
        }

        public async Task<Step> UpdateAsync(Step item)
        {
            var query =
               "update recipe_steps set ordinal = @ordinal, instructions = @instructions, modifiedby = @modifiedby, modifieddate = now() where id = @id returning *";

            var record = new
            {
                id = item.ID,
                ordinal = item.Ordinal,
                recipeid = item.RecipeId,
                instructions = item.Instructions,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<Step>(query, record);
            }
        }
    }
}
