using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace VetrinaDolci.WebAPI
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
            // IdentityModelEventSource.ShowPII = true;

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VetrinaDolci.WebAPI", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"vetrinadolci.webapi", "Demo VetrinaDolci.WebAPI - full access"}
                            }
                        }
                    }
                });
                
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration.GetSection("Identity:Authority").Get<string>();
                    options.RequireHttpsMetadata = Configuration.GetSection("Identity:RequireHttpsMetadata").Get<bool>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "vetrinadolci.webapi");
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy( builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddDbContext<ApplicationContext>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VetrinaDolci.WebAPI v1");

                c.OAuthClientId("demo_api_swagger");
                c.OAuthAppName("Demo VetrinaDolci.WebAPI - Swagger");
                c.OAuthUsePkce();
            });

            if (Configuration.GetSection("Identity:UseHttpsRedirection").Get<bool>())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope");
            });

            UpdateDatabaseMigrate<ApplicationContext>(app, env);
        }

        public static async void UpdateDatabaseMigrate<T>(IApplicationBuilder app, IWebHostEnvironment env) where T : DbContext
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<T>>();

                string databaseName;
                if (context.Database.ProviderName.Split('.').Last() == "Sqlite")
                {
                    databaseName = Path.GetFileName(context.Database.GetDbConnection().DataSource);
                }
                else
                {
                    databaseName = context.Database.GetDbConnection().Database;
                }

                if (!await (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).ExistsAsync())
                {
                    logger.LogDebug($"Database {databaseName} non trovato. Inizializzazione database con migrazione.");
                    await context.Database.MigrateAsync();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    await SeedHelper.SeedFromCsv(db);
                }
                logger.LogDebug($"Database {databaseName} found!");
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    logger.LogDebug($"Database {databaseName} not updated. MigrateAsync...");
                    await context.Database.MigrateAsync();
                    logger.LogDebug($"Database Updated.");
                }

                logger.LogDebug($"Check database {databaseName} OK.");
            }
        }
    }
}
