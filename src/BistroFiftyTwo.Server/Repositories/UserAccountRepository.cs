using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class UserAccountRepository : BaseRepository, IUserAccountRepository
    {
        public UserAccountRepository(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }


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

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<UserAccount>(query, values);
            }
        }

        public async Task DeleteAsync(UserAccount item)
        {
            using (var connection = await CreateConnection())
            {
                await connection.ExecuteAsync("delete from organization_accounts where id = @id", new {id = item.ID});
            }
        }

        public async Task<UserAccount> GetAsync(Guid id)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<UserAccount>(
                    "select * from organization_accounts where id = @id",
                    new {id});
            }
        }

        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QueryAsync<UserAccount>("select * from organization_accounts ");
            }
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<UserAccount>(
                    "select * from organization_accounts where email = @email",
                    new {email});
            }
        }

        public async Task<UserAccount> GetByLoginAsync(string login)
        {
            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<UserAccount>(
                    "select * from organization_accounts where userlogin = @login",
                    new {login});
            }
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

            using (var connection = await CreateConnection())
            {
                return await connection.QuerySingleAsync<UserAccount>(query, values);
            }
        }

        public void Dispose()
        {
        }
    }
}