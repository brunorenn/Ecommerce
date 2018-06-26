using Catalog.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Catalog.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureSwagger(services);
            ConfigureDatabase(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Hsts protocol force https all requests
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            ConfigureSwaggerUI(app, env, loggerFactory);
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Ecommerce - catalog API document",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API",
                    TermsOfService = "Terms Of Service"
                });
            });
        }

        public void ConfigureSwaggerUI(IApplicationBuilder app,
                             IHostingEnvironment env,
                             ILoggerFactory loggerFactory)
        {
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API V1");
            });
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"], sqlServerOptionsAction: sqlOptions =>
                {
                    //Configure migrations
                    sqlOptions.
                    MigrationsAssembly(
                    typeof(Startup).
                    GetTypeInfo().
                    Assembly.
                    GetName().Name);

                    //Configuring Connection Resiliency:
                    sqlOptions.
                     EnableRetryOnFailure(maxRetryCount: 5,
                     maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);

                    // Changing default behavior when client evaluation occurs to throw.
                    // Default in EFCore would be to log warning when client evaluation is done.
                    options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                });
            });
        }
    }
}
