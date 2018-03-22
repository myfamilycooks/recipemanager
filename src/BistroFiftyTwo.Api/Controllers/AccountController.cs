using System;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        public AccountController(IUserAccountService userAccountService, IRoleService roleService,
            IConfiguration configuration)
        {
            UserAccountService = userAccountService;
            Configuration = configuration;
            RoleService = roleService;
        }

        protected IUserAccountService UserAccountService { get; set; }
        protected IRoleService RoleService { get; set; }
        protected IConfiguration Configuration { get; set; }

        [AllowAnonymous]
        [Route("new")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountModel createAccount)
        {
            //nts if this should go to a fluent validation thing...
            if (string.IsNullOrEmpty(createAccount.Email)) return BadRequest(BistroFiftyTwoError.MissingField("email"));
            if (string.IsNullOrEmpty(createAccount.FullName))
                return BadRequest(BistroFiftyTwoError.MissingField("fullname"));
            if (string.IsNullOrEmpty(createAccount.Login)) return BadRequest(BistroFiftyTwoError.MissingField("login"));
            if (string.IsNullOrEmpty(createAccount.Password))
                return BadRequest(BistroFiftyTwoError.MissingField("password"));

            var inviteCode = default(Guid);
            if (!Guid.TryParse(createAccount.InvitationCode, out inviteCode))
                return BadRequest(BistroFiftyTwoError.MissingField("invitationCode"));

            // for now do the stupid, just hard code the inivite code.
            if (inviteCode != Guid.Parse(Configuration["InvitationCode"]))
                return BadRequest(BistroFiftyTwoError.Invalid("invitationCode", createAccount.InvitationCode));

            var newUserAccount = new UserAccount
            {
                Email = createAccount.Email,
                Fullname = createAccount.FullName,
                UserLogin = createAccount.Login,
                AccountPassword = createAccount.Password
            };

            var userAccount = await UserAccountService.Create(newUserAccount);

            await RoleService.GrantDefaultRoles(userAccount.ID);

            var securedAccount = new SecuredUserAccount
            {
                Email = userAccount.Email,
                Fullname = userAccount.Fullname,
                ID = userAccount.ID,
                IsDisabled = userAccount.IsDisabled,
                IsLocked = userAccount.IsLocked,
                UserLogin = userAccount.UserLogin
            };

            return Created($"api/accounts/{userAccount.ID}", securedAccount);
        }
    }
}