using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;
using Npgsql;

namespace BistroFiftyTwo.Server.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        public UserAccountRepository(IConfigurationService configurationService)
        {
            Connection = new NpgsqlConnection();
            Connection.ConnectionString = configurationService.Get("Data:RecipeX:ConnectionString");
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public async Task<UserAccount> CreateAsync(UserAccount item)
        {
            var query = @"insert into organization_accounts
                (userlogin, email, accountpassword, createdby, modifiedby, fullname, salt, passwordformat)
                values 
                (@userlogin, @email, @accountpassword, @createdby, @modifiedby, @fullname, @salt, @passwordformat)
                returning *";

            var values = new
            {
                userlogin = item.UserLogin,
                email = item.Email,
                fullname = item.Fullname,
                accountpassword = item.AccountPassword,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy,
                salt = item.Salt,
                passwordformat = item.PasswordFormat
            };

            return await Connection.QuerySingleAsync<UserAccount>(query, values);
        }

        public async Task DeleteAsync(UserAccount item)
        {
            await Connection.ExecuteAsync("delete from organization_accounts where id = @id", new {id = item.ID});
        }

        public async Task<UserAccount> GetAsync(Guid id)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from organization_accounts where id = @id",
                new {id});
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            return await Connection.QueryAsync<UserAccount>("select * from organization_accounts");
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from organization_accounts where id = @email",
                new {email});
        }

        public async Task<UserAccount> GetByLoginAsync(string login)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from organization_accounts where userlogin = @login",
                new {login});
        }

        public async Task<UserAccount> UpdateAsync(UserAccount item)
        {
            var query = @"update organization_accounts set 
                             userlogin = @userlogin,
                             email = @email,
                             accountpassword = @accountpassword,
                             islocked = @islocked,
                             isdisabled = @isdisabled,
                             modifieddate = @modifieddate,
                             modifiedby = @modifiedby,
                             fullname = @fullname,
                             salt = @salt,
                             passwordformat = @passwordformat
                            where id = @id
                            returning *;";

            var values = new
            {
                userlogin = item.UserLogin,
                email = item.Email,
                accountpassword = item.AccountPassword,
                islocked = item.IsLocked,
                isdisabled = item.IsDisabled,
                modifieddate = item.ModifiedDate,
                modifiedby = item.ModifiedBy,
                fullname = item.Fullname,
                salt = item.Salt,
                passwordformat = item.PasswordFormat,
                id = item.ID
            };

            return await Connection.QuerySingleAsync<UserAccount>(query, values);
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();

            Connection.Dispose();
        }
    }
}