using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Profile")]

    public class ProfileController : Controller
    {
        [Route("whoami")]
        public async Task<IActionResult> WhoAmI()
        {
            return Ok(new {chef = "James Hetfield", Band = "Metallica"});
        }

        [Authorize, HttpGet("~/api/test")]
        public IActionResult GetMessage()
        {
            return Json(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.Identity.Name
            });

        }
    }
}