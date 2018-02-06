using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BistroFiftyTwo.Server.Repositories
{
   

    public class OrganizationRepository : BaseRepository, IOrganizationRepository
    {
        public OrganizationRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }
        public async Task<Organization> GetAsync(Guid id)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.GetAsync<Organization>(id);
            }
        }

        public async Task<Organization> CreateAsync(Organization item)
        {
            var query =
                "insert into organizations (name, urlkey, description, orgtype, owner, createdby, modifiedby) values (@name, @urlkey, @description, @orgtype, @owner, @createdby, @modifiedby) returning *;";

            var record = new
            {
                name = item.Name,
                urlkey = item.UrlKey,
                description = item.Description,
                owner = item.Owner,
                orgtype = item.OrgType,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Organization>(query, record);
            }
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            using (var connection = await CreateConnection())
            {
                return await connection.GetAllAsync<Organization>();
            }
        }

        public async Task<Organization> UpdateAsync(Organization item)
        {
            using (var connection = await CreateConnection())
            {
                await connection.UpdateAsync<Organization>(item);
            }

            return await GetByUrlKeyAsync(item.UrlKey);
        }

        public async Task DeleteAsync(Organization item)
        {
            using (var connection = await CreateConnection())
            {
                await connection.DeleteAsync(item);
            }
        }

        public async Task<Organization> GetByUrlKeyAsync(string urlKey)
        {

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Organization>("select * from organizations where urlkey = @urlkey",
        new { urlkey = urlKey });
            }

        }

        public void Dispose()
        {
        }
    }

    
}
