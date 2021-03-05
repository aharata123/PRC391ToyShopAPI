using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Handlers;
using PRC391ToyShopAPI.Repositories;
using PRC391ToyShopAPI.Repositories.Repository;
using PRC391ToyShopAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddDbContext<PRC391_ToyShopContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ToyShopDB")));

            services.AddSwaggerGen(gen => {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Toy Shop API", Version = "v1.0" });
            });

            // Auto Mapper
            
            services.AddAutoMapper(typeof(Startup));


            // Dependency Injection

            // Repository
            services.AddScoped<IToyRepository, ToyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            // Service
            services.AddScoped<IToyService, ToyService>();
            services.AddScoped<IAccountService, AccountServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

       /*     app.UseAuthentication();*/

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI( ui => ui.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Toy Shop API Endpoint"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
