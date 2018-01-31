using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Profile")]
    public class ProfileController : Controller
    {
        protected IUserAccountService UserAccountService { get; set; }

        public ProfileController(IUserAccountService userAccountService)
        {
            UserAccountService = userAccountService;
        }

        [Route("whoami"), HttpGet]
        public async Task<IActionResult> WhoAmI()
        {
            var claimsUser = User.Identity as ClaimsIdentity;
            var id = claimsUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid).Value;
            var actualUser = await UserAccountService.Get(Guid.Parse(id));

            return Ok(new { user = actualUser, claims = claimsUser});
        }

        [Route("Secure"), HttpGet]
        public async Task<IActionResult> Secure()
        {
            var claimsUser = User.Identity as ClaimsIdentity;
            var id = claimsUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid).Value;
            var actualUser = await UserAccountService.Get(Guid.Parse(id));

            if(actualUser.PasswordFormat == (int)PasswordFormat.Clear)
               actualUser = await UserAccountService.Secure(actualUser, actualUser.AccountPassword);

            return Ok(actualUser);
        }
    }
}