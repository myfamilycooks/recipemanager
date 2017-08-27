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
    public class StepRepository : IStepRepository
    {
        public StepRepository(IConfigurationService configurationService) 
        {
            Connection = new NpgsqlConnection(configurationService.Get("Data:Recipe:ConnectionString"));
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }
        public async Task<Step> CreateAsync(Step item)
        {
            var query =
                "insert into recipe_steps (ordinal, recipeid, instructions, createdby, modifiedby) values (@ordinal, @recipeid, @instructions, @createdbby, @modifiedby) returning *";

            var record = new
            {
                ordinal = item.Ordinal,
                recipeid = item.RecipeId,
                instructions = item.Instructions,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            return await Connection.QuerySingleAsync<Step>(query, record);
        }

        public async Task DeleteAsync(Step item)
        {
            var query = "delete from recipe_steps where id = @id";

            await Connection.ExecuteAsync(query, new { id = item.ID });
        }

        public async Task<IEnumerable<Step>> GetAllAsync()
        {
            var query =
                "select id, ordinal, recipeid, instructions, createddate, createdby, modifieddate, modifiedby from recipe_steps";

            return await Connection.QueryAsync<Step>(query);
        }

        public async Task<Step> GetAsync(Guid id)
        {
            var query =
                "select id, ordinal, recipeid, instructions, createddate, createdby, modifieddate, modifiedby from recipe_steps id = @id";
            
            return await Connection.QuerySingleAsync<Step>(query, new {id});
        }

        public async Task<IEnumerable<Step>> GetByRecipeIdAsync(Guid recipeId)
        {
            return await Connection.QueryAsync<Step>("select * from recipe_steps where recipeid = @recipeId", new { recipeId });
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

            return await Connection.QuerySingleAsync<Step>(query, record);
        }
    }
}
