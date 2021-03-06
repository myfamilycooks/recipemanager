﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Repositories;

namespace BistroFiftyTwo.Server.Services
{
    public class UserAccountService : IUserAccountService, IDisposable
    {
        public UserAccountService(IUserAccountRepository repository, IEncryptionService encryptionService)
        {
            Repository = repository;
            EncryptionService = encryptionService;
        }

        protected IUserAccountRepository Repository { get; set; }
        protected IEncryptionService EncryptionService { get; set; }

        public void Dispose()
        {
            Repository.Dispose();
        }

        public async Task<UserAccount> Create(UserAccount item)
        {
            var salt = EncryptionService.GenerateSalt();
            item.Salt = salt;
            item.AccountPassword = EncryptionService.SlowOneWayHash(item.AccountPassword, salt);
            item.PasswordFormat = (int) PasswordFormat.Hashed;
            item.CreatedBy = "chef";
            item.CreatedDate = DateTime.UtcNow;
            item.ModifiedBy = "chef";
            item.ModifiedDate = DateTime.UtcNow;

            return await Repository.CreateAsync(item);
        }

        public async Task Delete(UserAccount item)
        {
            await Repository.DeleteAsync(item);
        }

        public async Task<UserAccount> Get(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<IEnumerable<UserAccount>> GetAll()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<UserAccount> GetByEmail(string email)
        {
            return await Repository.GetByEmailAsync(email);
        }

        public async Task<UserAccount> GetByLogin(string login)
        {
            return await Repository.GetByLoginAsync(login);
        }

        public async Task<UserAccount> Update(UserAccount item)
        {
            var originalAccount = await Get(item.ID);
            item.AccountPassword = originalAccount.AccountPassword;
            item.PasswordFormat = (int) PasswordFormat.Hashed;

            return await Repository.UpdateAsync(item);
        }

        public async Task<UserAccount> Secure(UserAccount item, string newPassword)
        {
            var salt = EncryptionService.GenerateSalt();

            if (!string.IsNullOrEmpty(newPassword))
                item.AccountPassword = newPassword;
            // todo launch audit event..

            var encrypted = EncryptionService.SlowOneWayHash(item.AccountPassword, salt);

            item.AccountPassword = encrypted;
            item.Salt = salt;
            item.PasswordFormat = (int) PasswordFormat.Hashed;

            return await Repository.UpdateAsync(item);
        }

        public async Task<UserAccount> Login(string login, string password)
        {
            var userAccount = await Repository.GetByLoginAsync(login);

            if (userAccount == null) return null;

            if (userAccount.PasswordFormat != (int) PasswordFormat.Hashed)
                if (userAccount.AccountPassword.Equals(password))
                    return userAccount;
            // hash passed in pw...
            var hashedPassword = EncryptionService.SlowOneWayHash(password, userAccount.Salt);

            if (userAccount.AccountPassword.Equals(hashedPassword))
                return userAccount;
            return null;
        }
    }
}