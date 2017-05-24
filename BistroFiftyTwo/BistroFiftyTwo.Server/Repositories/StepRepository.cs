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
    public interface IStepRepository : IDataRepository<Step>
    {
        Task<IEnumerable<Step>> GetByRecipeIdAsync(Guid recipeId);
    }
    public class StepRepository : AutomaticDataRepository<Step>, IStepRepository
    {
        public StepRepository(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public async Task<IEnumerable<Step>> GetByRecipeIdAsync(Guid recipeId)
        {
            return await Connection.QueryAsync<Step>("select * from recipe_steps where recipeid = @recipeId", new { recipeId });
        }
    }
}
