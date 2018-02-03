using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BistroFiftyTwo.Api.Models;
using BistroFiftyTwo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BistroFiftyTwo.Api.Controllers
{
    public class TokenController : Controller
    {
        protected IUserAccountService UserAccountService { get; set; }
        protected IConfiguration Configuration { get; set; }

        public TokenController(IUserAccountService userAccountService, IConfiguration configuration)
        {
            UserAccountService = userAccountService;
            Configuration = configuration;
        }

        [AllowAnonymous, Route("token"), HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var account = await UserAccountService.Login(loginViewModel.Username, loginViewModel.Password);

            if (account == null) return Unauthorized();

            var claims = new[]
            {
                new Claim("b52accountName", account.UserLogin), 
                new Claim(JwtRegisteredClaimNames.Sub, account.UserLogin),
                new Claim(JwtRegisteredClaimNames.Sid, account.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken
            (
                issuer: Configuration["Issuer"],
                audience: Configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])),
                     SecurityAlgorithms.HmacSha256)
            );

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}