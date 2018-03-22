using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class OrganizationMemberRepository : BaseRepository, IOrganizationMemberRepository
    {
        public OrganizationMemberRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public async Task<OrganizationMember> GetAsync(Guid id)
        {
            var query =
                "select * from organization_members where id = @id";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<OrganizationMember>(query, new {id});
            }
        }

        public async Task<OrganizationMember> GetAsync(Guid organizationId, Guid accountId)
        {
            var query =
                "select * from organization_members where organizationid = @organizationid AND accountid = @accountid;";

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<OrganizationMember>(query,
                    new {organizationid = organizationId, accountid = accountId});
            }
        }


        public async Task<OrganizationMember> CreateAsync(OrganizationMember item)
        {
            var query =
                "insert into organization_members (organizationid, accountid, accesslevel, membershipstatus, createdby, modifiedby) values (@organizationid, @accountid, @accesslevel, @membershipstatus, @createdby, @modifiedby) returning *;";

            var record = new
            {
                organizationid = item.OrganizationId,
                accountid = item.AccountId,
                accesslevel = item.AccessLevel,
                membershipstatus = item.MembershipStatus,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<OrganizationMember>(query, record);
            }
        }

        public async Task<IEnumerable<OrganizationMember>> GetAllAsync()
        {
            var query =
                "select * from organization_members";

            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<OrganizationMember>(query);
            }
        }

        public async Task<OrganizationMember> UpdateAsync(OrganizationMember item)
        {
            var query =
                "update organization_members set accesslevel = @accesslevel, membershipstatus = @membershipstatus, modifieddate = now(), modifiedby = @modifiedby where id  = @id returning *;";

            var record = new
            {
                accesslevel = item.AccessLevel,
                membershipstatus = item.MembershipStatus,
                modifiedby = item.ModifiedBy,
                id = item.ID
            };

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<OrganizationMember>(query, record);
            }
        }

        public async Task DeleteAsync(OrganizationMember item)
        {
            var query =
                "delete from organization_members where id = @id";

            using (var connection = await CreateConnection())
            {
                await connection.QueryAsync(query, new {id = item.ID});
            }
        }
    }
}