// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users.Concat(GetUser()).ToList())
                ;

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            // add cors with DI
            //services.AddSingleton<ICorsPolicyService>((container) => {
            //    var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
            //    return new DefaultCorsPolicyService(logger)
            //    {
            //        AllowedOrigins = { "http://localhost:4200" }
            //    };
            //});
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private List<TestUser> GetUser() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "918727",
                    Username = "Luana@email.com",
                    Password = "Luana",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Luana Pasticceria"),
                        new Claim(JwtClaimTypes.GivenName, "Luana"),
                        new Claim(JwtClaimTypes.FamilyName, "Pasticceria"),
                        new Claim(JwtClaimTypes.Email, "LuanaPasticceria@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    }
                },
                new TestUser
                {
                    SubjectId = "918727",
                    Username = "Maria@email.com",
                    Password = "Maria",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Maria Pasticceria"),
                        new Claim(JwtClaimTypes.GivenName, "Maria"),
                        new Claim(JwtClaimTypes.FamilyName, "Pasticceria"),
                        new Claim(JwtClaimTypes.Email, "LuanaPasticceria@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    }
                },
            };

    }
}
