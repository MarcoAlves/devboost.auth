using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        private const string ConnectionName = "IdentityConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString(ConnectionName);
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();


            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options => { options.ConfigureDbContext = builder => builder.UseSqlite(connectionString, db => db.MigrationsAssembly(migrationsAssembly)); })
                    .AddOperationalStore(options => { options.ConfigureDbContext = builder => builder.UseSqlite(connectionString, db => db.MigrationsAssembly(migrationsAssembly)); });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("POC OAuth 28/08 DevBoost");
                });
            });
        }
    }
}
