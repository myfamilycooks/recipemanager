using System;
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
            item.Password = EncryptionService.SlowOneWayHash(item.Password, salt);
            item.PasswordFormat = (int) PasswordFormat.Hashed;
            item.CreatedBy = "chef";
            item.CreatedDate = DateTime.UtcNow;
            item.ModifiedBy = "chef";
            item.ModifiedDate = DateTime.UtcNow;

            return await Repository.Create(item);
        }

        public async Task Delete(UserAccount item)
        {
            await Repository.Delete(item);
        }

        public async Task<UserAccount> Get(Guid id)
        {
            return await Repository.Get(id);
        }

        public async Task<IEnumerable<UserAccount>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<UserAccount> GetByEmail(string email)
        {
            return await Repository.GetByEmail(email);
        }

        public async Task<UserAccount> GetByLogin(string login)
        {
            return await Repository.GetByLogin(login);
        }

        public async Task<UserAccount> Update(UserAccount item)
        {
            var originalAccount = await Get(item.ID);
            item.Password = originalAccount.Password;
            item.PasswordFormat = (int) PasswordFormat.Hashed;

            return await Repository.Update(item);
        }

        public async Task<UserAccount> Secure(UserAccount item, string newPassword)
        {
            var salt = EncryptionService.GenerateSalt();

            if (!string.IsNullOrEmpty(newPassword))
                item.Password = newPassword;
            // todo launch audit event..

            var encrypted = EncryptionService.SlowOneWayHash(item.Password, salt);

            item.Password = encrypted;
            item.Salt = salt;
            item.PasswordFormat = (int) PasswordFormat.Hashed;

            return await Repository.Update(item);
        }

        public async Task<UserAccount> Login(string login, string password)
        {
            var userAccount = await Repository.GetByLogin(login);

            if (userAccount.PasswordFormat != (int) PasswordFormat.Hashed)
                if (userAccount.Password.Equals(password))
                    return userAccount;
            // hash passed in pw...
            var hashedPassword = EncryptionService.SlowOneWayHash(password, userAccount.Salt);

            if (userAccount.Password.Equals(hashedPassword))
                return userAccount;
            return null;
        }
    }
}