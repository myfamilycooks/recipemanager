using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeManager.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/System")]
    public class SystemController : Controller
    {
        [HttpGet]
        [Route("Status")]
        public async Task<IActionResult> Status()
        {
            return Ok(new {Status = "Is Good", Version = "This One"});
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new {App = "Recipe Manager"});
        }

        [HttpGet]
        [Route("WhoAmI")]
        [Authorize]
        public async Task<IActionResult> WhoAmI()
        {
            return Ok(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}