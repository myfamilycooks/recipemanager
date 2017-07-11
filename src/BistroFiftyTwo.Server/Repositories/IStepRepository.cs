using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IStepRepository : IDataRepository<Step>
    {
        Task<IEnumerable<Step>> GetByRecipeIdAsync(Guid recipeId);
    }
}