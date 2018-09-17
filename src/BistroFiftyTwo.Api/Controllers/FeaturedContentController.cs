using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/featured")]
    [CustomExceptionFilter]
    public class FeaturedContentController : Controller
    {
        protected IRecipeService RecipeService { get; set; }
        protected ISearchService SearchService { get; set; }

        public FeaturedContentController(IRecipeService recipeService, ISearchService searchService)
        {
            RecipeService = recipeService;
            SearchService = searchService;
        }
        [Authorize, HttpGet]
        public async Task<IActionResult> Get()
        {
            var featuredRecipes = await SearchService.SearchRecipes(new Server.Entities.RecipeQuery {SearchText = "Pepper"});

            return Ok(new { Recipes = featuredRecipes.Recipes}); 
        }
    }
}