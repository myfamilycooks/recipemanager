using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BistroFiftyTwo.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseWhen(context => context.Request.Path.StartsWithSegments(new PathString("/api")), branch =>
            {
                branch.UseCors(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                branch.UseOAuthValidation(new OAuthValidationOptions
                {
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true
                });
            });

            app.UseOpenIdConnectServer(options =>
            {
                options.TokenEndpointPath = "/connect/token";
                options.AllowInsecureHttp = true;
                options.SigningCredentials.AddEphemeralKey();
                options.Provider.OnValidateTokenRequest = context =>
                {
                    if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
                    {
                        context.Reject(
                            OpenIdConnectConstants.Errors.UnsupportedGrantType,
                            "Only grant_type=password and refresh_token " +
                            "requests are accepted by this server.");

                        return Task.FromResult(0);
                    }

                    context.Skip();

                    return Task.FromResult(0);
                };
                // Implement OnHandleTokenRequest to support token requests.
                options.Provider.OnHandleTokenRequest = context =>
                {
                    // Only handle grant_type=password token requests and let the
                    // OpenID Connect server middleware handle the other grant types.
                    if (context.Request.IsPasswordGrantType())
                    {
                        // Implement context.Request.Username/context.Request.Password validation here.
                        // Note: you can call context Reject() to indicate that authentication failed.
                        // Using password derivation and time-constant comparer is STRONGLY recommended.
                        if (!string.Equals(context.Request.Username, "chef", StringComparison.Ordinal) ||
                            !string.Equals(context.Request.Password, "mustard", StringComparison.Ordinal))
                        {
                            context.Reject(
                                OpenIdConnectConstants.Errors.InvalidGrant,
                                "Invalid user credentials.");

                            return Task.FromResult(0);
                        }

                        var identity = new ClaimsIdentity(context.Options.AuthenticationScheme,
                            OpenIdConnectConstants.Claims.Name,
                            OpenIdConnectConstants.Claims.Role);

                        // Add the mandatory subject/user identifier claim.
                        identity.AddClaim(OpenIdConnectConstants.Claims.Subject, $"{Guid.NewGuid()}");

                        // By default, claims are not serialized in the access/identity tokens.
                        // Use the overload taking a "destinations" parameter to make sure
                        // your claims are correctly inserted in the appropriate tokens.
                        identity.AddClaim("urn:properName", "Chef Hetfield",
                            OpenIdConnectConstants.Destinations.AccessToken,
                            OpenIdConnectConstants.Destinations.IdentityToken);
                        identity.AddClaim("urn:email", "chef@myfamilycooks.com",
                            OpenIdConnectConstants.Destinations.AccessToken,
                            OpenIdConnectConstants.Destinations.IdentityToken);

                        var ticket = new AuthenticationTicket(
                            new ClaimsPrincipal(identity),
                            new AuthenticationProperties(),
                            context.Options.AuthenticationScheme);

                        // Call SetScopes with the list of scopes you want to grant
                        // (specify offline_access to issue a refresh token).
                        ticket.SetScopes(
                            /* openid: */ OpenIdConnectConstants.Scopes.OpenId,
                            OpenIdConnectConstants.Scopes.Profile,
                            OpenIdConnectConstants.Scopes.OfflineAccess);

                        context.Validate(ticket);
                    }

                    return Task.FromResult(0);
                };
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}