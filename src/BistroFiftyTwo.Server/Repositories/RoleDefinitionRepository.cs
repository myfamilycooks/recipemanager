using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Npgsql;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RoleDefinitionRepository : IRoleDefinitionRepository
    {
        public RoleDefinitionRepository(IConfigurationService configurationService)
        {
            Connection = new NpgsqlConnection(configurationService.Get("Data:IdentityConnection:ConnectionString"));
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public async Task<RoleDefinition> CreateAsync(RoleDefinition item)
        {
            var query = @"insert into role_definitions (name, fullname, description, createdby, modifiedby)
                           values (@name, @fullname, @description, @createdby, @modifiedby)
                         returning *";
            var row = new
            {
                name = item.Name,
                fullname = item.Fullname,
                description = item.Description,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            return await Connection.QuerySingleAsync<RoleDefinition>(query, row);
        }

        public async Task DeleteAsync(RoleDefinition item)
        {
            await Connection.ExecuteAsync("delete from role_definitions where id = @id", new {id = item.ID});
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }

        public async Task<RoleDefinition> GetAsync(Guid id)
        {
            return
                await Connection.QuerySingleOrDefaultAsync<RoleDefinition>(
                    "select * from role_definitions where id = @id", new {id});
        }

        public async Task<IEnumerable<RoleDefinition>> GetAllAsync()
        {
            return await Connection.QueryAsync<RoleDefinition>("select * from role_definitions");
        }

        public async Task<RoleDefinition> UpdateAsync(RoleDefinition item)
        {
            var query = @"
                         update role_definitions
                            name = @name, fullname = @fullname, description = @description, modifiedby = @modifiedby, modifieddate = @modifieddate
                         where id = @id
                         returning *;";
            var row = new
            {
                name = item.Name,
                fullname = item.Fullname,
                description = item.Description,
                modifiedby = item.ModifiedBy,
                modifieddate = item.ModifiedDate
            };

            return await Connection.QuerySingleAsync<RoleDefinition>(query, row);
        }
    }
}