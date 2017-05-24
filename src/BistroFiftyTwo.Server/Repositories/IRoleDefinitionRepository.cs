using System;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IRoleDefinitionRepository : IDataRepository<RoleDefinition>, IDisposable
    {
    }
}