using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BistroFiftyTwo.Server.Services
{
    public interface ISecurityService
    {
        Task<UserAccount> GetCurrentUser();
        Task<string> GetCurrentUserName();
    }
    public class SecurityService : ISecurityService
    {
        protected IPrincipal Principal { get; set; }
        protected IMemoryCache MemoryCache { get; set; }
        protected IUserAccountService UserAccountService { get; set; }
        public SecurityService(IPrincipal principal, IUserAccountService userAccountService, IMemoryCache memoryCache)
        {
            Principal = principal;
            UserAccountService = userAccountService;
            MemoryCache = memoryCache;
        }
        public async Task<UserAccount> GetCurrentUser()
        {
            var id = ((ClaimsIdentity)Principal.Identity).Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid).Value;

            return await MemoryCache.GetOrCreateAsync<UserAccount>($"User{id}", async entry =>
            {
                var userAccount = await UserAccountService.Get(Guid.Parse(id));
                entry.AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(20);
                entry.Value = userAccount;

                return userAccount;
            }); 
        }
        
        public async Task<string> GetCurrentUserName()
        {
            var claimsPrincipal = Principal as ClaimsPrincipal;
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "b52accountName");
            return claim.Value;
        }
    }
}