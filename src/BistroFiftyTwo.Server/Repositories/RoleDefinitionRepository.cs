using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;
using Npgsql;

namespace BistroFiftyTwo.Server.Repositories
{
    public class RoleDefinitionRepository : BaseRepository, IRoleDefinitionRepository
    {
        public RoleDefinitionRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
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

            using(var connection = await CreateConnection()) { 
            return await Connection.QuerySingleAsync<RoleDefinition>(query, row);
            }
        }

        public async Task DeleteAsync(RoleDefinition item)
        {
            using(var connection = await CreateConnection()) { 
                await connection.ExecuteAsync("delete from role_definitions where id = @id", new {id = item.ID});
            }
        }

        public void Dispose()
        {

        }

        public async Task<RoleDefinition> GetAsync(Guid id)
        {
            using(var connection = await CreateConnection()) { 
            return
                await connection.QuerySingleOrDefaultAsync<RoleDefinition>(
                    "select * from role_definitions where id = @id", new {id});
            }
        }

        public async Task<IEnumerable<RoleDefinition>> GetAllAsync()
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<RoleDefinition>("select * from role_definitions");
            }
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

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<RoleDefinition>(query, row);
            }
        }
    }
}