using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        public SearchController(ISearchService searchService)
        {
            SearchService = searchService;
        }

        private ISearchService SearchService { get; }

        [Authorize]
        [HttpGet("suggestions")]
        public async Task<IActionResult> Suggestions(string term)
        {
            return Ok(await SearchService.Suggestions(term));
        }

        [Authorize]
        [HttpPost("recipes")]
        public async Task<IActionResult> Recipes([FromBody] RecipeQuery query)
        {
            return Ok(await SearchService.SearchRecipes(query));
        }
    }
}