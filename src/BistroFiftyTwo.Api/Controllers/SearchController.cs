using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private ISearchService SearchService { get; set; }
        public SearchController(ISearchService searchService) { SearchService = searchService; }

        [Authorize, HttpGet("recipes")]
        public async Task<IActionResult> Recipes()
        {
            return Ok(await SearchService.SearchRecipes(null));
        }

        [Authorize, HttpPost("recipes")]
        public async Task<IActionResult> Recipes([FromBody]RecipeQuery query)
        {
            return Ok(await SearchService.SearchRecipes(query));
        }
    }

   
}