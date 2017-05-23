﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Npgsql;
using Dapper;
namespace BistroFiftyTwo.Server.Repositories
{
    public interface IUserRoleRepository : IDisposable
    {
        Task<IEnumerable<UserRole>> GetByUserID(Guid userId);
        Task<IEnumerable<UserRole>> GetByRoleID(Guid roleId);
        Task<UserRole> Create(UserRole item);
        Task<IEnumerable<UserRole>> GetAll();
        Task<UserRole> Update(UserRole item);
        Task Delete(UserRole item);
        Task<UserRole> GetUserRole(Guid userid, Guid roleId);
    }

    public class UserRoleRepository : IUserRoleRepository
    {
        public UserRoleRepository(IConfigurationService configurationService)
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

        public async Task<IEnumerable<UserRole>> GetByUserID(Guid userId)
        {
            var query = "select * from user_roles where userid = @userId";
            var param = new {userId};

            return await Connection.QueryAsync<UserRole>(query, param);
        }

        public async Task<IEnumerable<UserRole>> GetByRoleID(Guid roleId)
        {
            var query = "select * from user_roles where roleid = @roleId";
            var param = new {roleId};

            return await Connection.QueryAsync<UserRole>(query, param);
        }

        public async Task<UserRole> Create(UserRole item)
        {
            var query =
                "insert into user_roles (userid, roleid, createdby, modifiedby) values (@userid, @roleid, @createdby, @modifiedby) returning *";
            var param =
                new
                {
                    userid = item.UserID,
                    roleid = item.RoleID,
                    createdby = item.CreatedBy,
                    modifiedby = item.ModifiedBy
                };

            return await Connection.QuerySingleAsync<UserRole>(query, param);
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            var query = "select * from user_roles";

            return await Connection.QueryAsync<UserRole>(query);
        }

        public async Task<UserRole> Update(UserRole item)
        {
            var query =
                "update user_roles set isdisabled=@isdisabled, modifieddate=@modifieddate, modifiedby = @modifiedby, effectiveenddate=@effectiveenddate where userid = @userid and roleid = @roleid";
            var param =
                new
                {
                    userid = item.UserID,
                    roleid = item.RoleID,
                    modifieddate = item.ModifiedDate,
                    modifiedby = item.ModifiedBy,
                    isdisabled = item.IsDisabled,
                    effectiveenddate = item.EffectiveEndDate
                };

            return await Connection.QuerySingleAsync<UserRole>(query, param);
        }

        public async Task Delete(UserRole item)
        {
            var query = "delete from user_roles where userid = @userId and roleid=@roleId";
            var param = new {roleId = item.RoleID, userId = item.UserID};

            await Connection.ExecuteAsync(query, param);
        }

        public async Task<UserRole> GetUserRole(Guid userid, Guid roleId)
        {
            var query = "select * from user_roles where userid = @userId and roleid = @roleid";
            var param = new {userId = userid, roleid = roleId};

            return await Connection.QuerySingleOrDefaultAsync<UserRole>(query, param);
        }
    }
}