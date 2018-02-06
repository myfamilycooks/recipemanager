using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BistroFiftyTwo.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace BistroFiftyTwo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "My Family Cooks API", });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cb => cb.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                };
            });
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["Caching:RedisAddress"];
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.Register(c => Configuration).As<IConfiguration>();

            builder.RegisterModule<BistroFiftyTwoServerModule>();
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Family Cooks API V1");
            });
        }
    }
}
