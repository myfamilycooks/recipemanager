using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeManager.WebApp.Entities;

namespace RecipeManager.WebApp.Data
{
    public interface IStepRepository : IDataRepository<Step>
    {
        Task<IEnumerable<Step>> GetByRecipeID(Guid recipeId);
    }
}