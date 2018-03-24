using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BistroFiftyTwo.Api.Controllers
{
    [Produces("application/json")]
    public class TokenController : Controller
    {
        public TokenController(IUserAccountService userAccountService, IRoleService roleService,
            IConfiguration configuration)
        {
            UserAccountService = userAccountService;
            Configuration = configuration;
            RoleService = roleService;
        }

        protected IUserAccountService UserAccountService { get; set; }
        protected IRoleService RoleService { get; set; }
        protected IConfiguration Configuration { get; set; }

        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        [CustomExceptionFilter]
        public async Task<IActionResult> Post([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var account = await UserAccountService.Login(loginViewModel.Username, loginViewModel.Password);

            if (account == null) return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim("b52accountName", account.UserLogin),
                new Claim(JwtRegisteredClaimNames.Sub, account.UserLogin),
                new Claim(JwtRegisteredClaimNames.Sid, account.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await RoleService.GetUserRoles(account.ID);

            roles.ForEach(r => { claims.Add(new Claim(r.Name, r.ID.ToString())); });


            var token = new JwtSecurityToken
            (
                Configuration["Issuer"],
                Configuration["Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}