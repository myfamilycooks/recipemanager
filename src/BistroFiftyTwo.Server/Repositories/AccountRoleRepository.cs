using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;
using Npgsql;

namespace BistroFiftyTwo.Server.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        public AccountRoleRepository(IConfigurationService configurationService)
        {
            Connection = new NpgsqlConnection();
            Connection.ConnectionString = configurationService.Get("Data:IdentityConnection:ConnectionString");
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }

        public async Task<IEnumerable<AccountRole>> GetByAccountIdAsync(Guid userId)
        {
            var query = "select * from account_roles where accountid = @userId";
            var param = new {userId};

            return await Connection.QueryAsync<AccountRole>(query, param);
        }

        public async Task<IEnumerable<AccountRole>> GetByRoleIDAsync(Guid roleId)
        {
            var query = "select * from account_roles where roleid = @roleId";
            var param = new {roleId};

            return await Connection.QueryAsync<AccountRole>(query, param);
        }

        public async Task<AccountRole> CreateAsync(AccountRole item)
        {
            var query =
                "insert into account_roles (accountid, roleid, createdby, modifiedby) values (@accountid, @roleid, @createdby, @modifiedby) returning *";
            var param =
                new
                {
                    accountid = item.AccountID,
                    roleid = item.RoleID,
                    createdby = item.CreatedBy,
                    modifiedby = item.ModifiedBy
                };

            return await Connection.QuerySingleAsync<AccountRole>(query, param);
        }

        public async Task<IEnumerable<AccountRole>> GetAllAsync()
        {
            var query = "select * from account_roles";

            return await Connection.QueryAsync<AccountRole>(query);
        }

        public async Task<AccountRole> UpdateAsync(AccountRole item)
        {
           throw new NotSupportedException();
        }

        public async Task DeleteAsync(AccountRole item)
        {
            var query = "delete from account_roles where accountid = @userId and roleid=@roleId";
            var param = new {roleId = item.RoleID, userId = item.AccountID};

            await Connection.ExecuteAsync(query, param);
        }

        public async Task<AccountRole> GetUserRoleAsync(Guid accountId, Guid roleId)
        {
            var query = "select * from account_roles where accountid = @accountid and roleid = @roleid";
            var param = new { accountid = accountId, roleid = roleId};

            return await Connection.QuerySingleOrDefaultAsync<AccountRole>(query, param);
        }
    }
}