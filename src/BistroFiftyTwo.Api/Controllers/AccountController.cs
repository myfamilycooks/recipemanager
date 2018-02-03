using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json"), Route("api/account")]
    public class AccountController : Controller
    {
        protected IUserAccountService UserAccountService { get; set; }
        protected IConfiguration Configuration { get;set; }
        public AccountController(IUserAccountService userAccountService, IConfiguration configuration)
        {
            UserAccountService = userAccountService;
            Configuration = configuration;
        }

        [AllowAnonymous, Route("new"), HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountModel createAccount)
        {
            if (ModelState.IsValid)
            {
                //nts if this should go to a fluent validation thing...
                if (String.IsNullOrEmpty(createAccount.Email)) return BadRequest();
                if (String.IsNullOrEmpty(createAccount.FullName)) return BadRequest();
                if (String.IsNullOrEmpty(createAccount.Login)) return BadRequest();
                if (String.IsNullOrEmpty(createAccount.Password)) return BadRequest();

                var inviteCode = default(Guid);
                if (!Guid.TryParse(createAccount.InvitationCode, out inviteCode)) return BadRequest();
                
                // for now do the stupid, just hard code the inivite code.
                if (inviteCode != Guid.Parse(Configuration["InvitationCode"])) return Forbid();

                var newUserAccount = new UserAccount()
                {
                    Email =  createAccount.Email,
                    Fullname = createAccount.FullName,
                    UserLogin = createAccount.Login,
                    AccountPassword = createAccount.Password
                };
                
                var userAccount = await UserAccountService.Create(newUserAccount);
                return Created($"api/accounts/{userAccount.ID}", userAccount);
            }

            return BadRequest();
        }
    }
}