using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using RecipeManager.WebApp.Entities;
using RecipeManager.WebApp.Services;

namespace RecipeManager.WebApp.Data
{
    public class StepRepository : IStepRepository
    {
        private IDatabase Database { get; set; }

        public StepRepository(IDatabase database)
        {
            Database = database;
        }

        public async Task<Step> Create(Step item)
        {
            var query =
                @"insert into recipe_steps (ordinal, recipeid, instruction, createddate, createdby, modifieddate, modifiedby) 
                          values (@ordinal, @recipeid, @instruction, @createddate, @createdby,@createddate, @createdby)
                          returning *;";

            var queryParams = new 
            {
                ordinal = item.Ordinal,
                recipeid = item.RecipeId,
                instruction = item.Instruction,
                createddate = item.CreatedDate,
                createdby = item.CreatedBy
            };

            await Database.Connect();
            return await Database.Connection.QueryFirstOrDefaultAsync<Step>(query, queryParams);
        }

        public async Task<IEnumerable<Step>> GetAll()
        {
            await Database.Connect();
            return await Database.Connection.QueryAsync<Step>("select * from recpie_steps");
        }

        public async Task<Step> Update(Step item)
        {
            throw new NotImplementedException();
        }

        public async Task<Step> Get(Guid id)
        {
            await Database.Connect();
            return await Database.Connection.QueryFirstOrDefaultAsync<Step>("select * from recpie_steps where id = @id",
                new { id = id });
        }

        public async Task Delete(Step item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Step>> GetByRecipeID(Guid recipeId)
        {
            await Database.Connect();
            return await Database.Connection.QueryAsync<Step>("select * from recpie_steps where recipeid = @recipeid",
                new {recipeid = recipeId});
        }
    }
}