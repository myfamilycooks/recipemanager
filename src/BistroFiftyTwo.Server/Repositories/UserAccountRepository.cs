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
            Connection.ConnectionString = configurationService.Get("Data:IdentityConnection:ConnectionString");
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public async Task<UserAccount> CreateAsync(UserAccount item)
        {
            var query = @"insert into user_accounts
                (login, email, password, createdby, modifiedby, fullname, salt, passwordformat)
                values 
                (@login, @email, @password, @createdby, @modifiedby, @fullname, @salt, @passwordformat)
                returning *";

            var values = new
            {
                login = item.Login,
                email = item.Email,
                fullname = item.Fullname,
                password = item.Password,
                createdby = item.CreatedBy,
                modifiedby = item.ModifiedBy,
                salt = item.Salt,
                passwordformat = item.PasswordFormat
            };

            return await Connection.QuerySingleAsync<UserAccount>(query, values);
        }

        public async Task DeleteAsync(UserAccount item)
        {
            await Connection.ExecuteAsync("delete from user_accounts where id = @id", new {id = item.ID});
        }

        public async Task<UserAccount> GetAsync(Guid id)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from user_accounts where id = @id",
                new {id});
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            return await Connection.QueryAsync<UserAccount>("select * from user_accounts");
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from user_accounts where id = @email",
                new {email});
        }

        public async Task<UserAccount> GetByLoginAsync(string login)
        {
            return await Connection.QuerySingleAsync<UserAccount>("select * from user_accounts where login = @login",
                new {login});
        }

        public async Task<UserAccount> UpdateAsync(UserAccount item)
        {
            var query = @"update user_accounts set 
                             login = @login,
                             email = @email,
                             password = @password,
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
                login = item.Login,
                email = item.Email,
                password = item.Password,
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