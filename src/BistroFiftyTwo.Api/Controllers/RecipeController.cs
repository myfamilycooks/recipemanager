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
    [Route("api")]
    public class PingController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("ping/secure")]
        public string PingSecured()
        {
            return "All good. You only get this message if you are authenticated.";
        }
    }

    public class Message
    {
        public DateTime Date { get; set; }
        public string Subject { get; set; }
    }

    [Route("api/messages")]
    public class MessagesController : Controller
    {
        [Authorize("read:messages")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new Message[]
            {
                new Message
                {
                    Date = DateTime.Now,
                    Subject = "Confirm Newsletter subscription"
                },
                new Message
                {
                    Date = DateTime.Now.AddDays(-1),
                    Subject = "Annual increase"
                }
            });
        }

        [Authorize("create:messages")]
        [HttpPost]
        public IActionResult Create([FromBody] Message message)
        {
            return Created("http://localhost:5000/api/messages/1", message);
        }
    }

    [Produces("application/json")]
    [Route("api/recipe")]
    public class RecipeController : Controller
    {
        private IRecipeService RecipeService { get; set; }
        public RecipeController(IRecipeService recipeService) { RecipeService = recipeService; }

        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var recipe = await RecipeService.GetByIdAsync(id);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            try
            {
                var recipe = await RecipeService.GetByKeyAsync(key);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody]Recipe recipe)
        {
            //try
            //{
            var createdRecipe = await RecipeService.CreateAsync(recipe);
            return Ok(createdRecipe);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500);
            //}

        }

    }
}