using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.WebApp.Entities;
using RecipeManager.WebApp.Services;

namespace RecipeManager.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Recipes")]
    public class RecipeController : Controller
    {
        private IDataRepository<Recipe> Repository { get; set; }

        public RecipeController(IDataRepository<Recipe> recipeDataRepository)
        {
            Repository = recipeDataRepository;
        }
        // GET: api/Recipe
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Repository.GetAll());
        }

        // GET: api/Recipe/5
        [HttpGet("{key}", Name = "Get")]
        public async Task<IActionResult> Get(string key)
        {
            return Ok(await Repository.Get(key));
        }
        
        // POST: api/Recipe
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
