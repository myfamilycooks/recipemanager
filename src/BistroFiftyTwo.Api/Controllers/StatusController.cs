using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/status")]
    [CustomExceptionFilter]
    public class StatusController : Controller
    {
        public StatusController(IConfigurationService configuration)
        {
            ConfigurationService = configuration; 
        }
         
        protected IConfigurationService ConfigurationService { get; set; }

        public async Task<IActionResult> Get()
        {
            var connection = ConfigurationService.Get("Data:RecipeX:ConnectionString");
            var parts = connection.Split(';');
            var connectionDictionary = new Dictionary<string, string>();

            foreach (var part in parts)
            {
                if(!String.IsNullOrEmpty(part)) { 
                    var keyValue = part.Split('=');
                    connectionDictionary.Add(keyValue[0], keyValue[1]);
                }
            }

            connectionDictionary["Password"] = "XXXXXXXXXXXXXXXXX";

            return Ok(new {Database = connectionDictionary, Redis = ConfigurationService.Get("Caching:RedisAddress")});
        }
    }
}
