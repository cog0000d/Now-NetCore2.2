using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System;
using System.Net;
using Common.Data.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Now.Data.DataContexts;
using Now.Data.Interfaces;
using Now.Data.Repositories;

namespace Now.WebHost
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IHostingEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    // Force Camel Case to JSON
                    opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                });

            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.Cookie.Name = Configuration["Authentication:ClientSecret"];
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = false;

                    options.ClientSecret = Configuration["Authentication:ClientSecret"];
                    options.ClientId = Configuration["Authentication:ClientId"];

                    options.ResponseType = "code id_token";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    //options.Scope.Add("phone");
                    options.Scope.Add("email");
                    options.Scope.Add("tenant");
                    options.Scope.Add("location");
                    //options.Scope.Add("offline_access");
                    options.Scope.Add("roles");
                    //options.Scope.Add(Global.AppSequence);


                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role
                    };

                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = n =>
                        {
                            // acr_values is where you can pass custom hints/params
                            //n.ProtocolMessage.AcrValues = "idp:Google";
                            n.ProtocolMessage.AcrValues =
                                    "tenant:" + Domain.GetSubDomain(Environment.WebRootPath).ToUpper();
                            return Task.FromResult(0);
                        }
                    };
                });

            services.AddScoped<INowRepository, NowRepository<NowDbContext>>();

            return services.BuildServiceProvider(true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });


            app.UseAuthentication();

            //app.UseTenantDbMapper();
            //app.UseSession(); //For Dynamic Theme Based on Tenant IMPORTANT: This session call MUST go before UseMvc() 

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
