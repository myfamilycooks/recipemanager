using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        [Route("whoami")]
        public async Task<IActionResult> WhoAmI()
        {
            return Ok(new {chef = "James Hetfield", Band = "Metallica"});
        }
    }
}