﻿using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRecipeRepository : IDataRepository<Recipe>, IDisposable
    {
        Task<Recipe> GetByKeyAsync(string key);
    }
}