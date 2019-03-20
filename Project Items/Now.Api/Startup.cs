using System;
using System.Threading.Tasks;
using Common.Data.Utilities;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Now.Api.Interfaces;
using Now.Api.Services;
using Now.Data.DataContexts;
using Now.Data.DataContexts.Schedule;
using Now.Data.Interfaces;
using Now.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace Now.Api
{
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        else
    //        {
    //            app.UseHsts();
    //        }

    //        app.UseHttpsRedirection();
    //        app.UseMvc();
    //    }
    //}

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


            //services.AddDbContext<NowDbContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("Now")));

            services.AddDbContext<ScheduleDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("Now")));


            services.AddCors();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); // Pagination

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<INowRepository, NowRepository<ScheduleDbContext>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Now Api", Version = "v1" });
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.Cookie.Name = Configuration["Authentication:ClientSecret"];
                    options.Cookie.SameSite = SameSiteMode.None;
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
            return services.BuildServiceProvider(true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeDatabase(app);
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

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

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Now Api");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var scheduleDbContext = serviceScope.ServiceProvider.GetRequiredService<ScheduleDbContext>();
                scheduleDbContext.Database.Migrate();

                if (!scheduleDbContext.Shifts.Any())
                {
                    var schedule = new ScheduleDataConfiguration(scheduleDbContext);

                    foreach (var continents in schedule.GetShifts())
                        scheduleDbContext.Shifts.Add(continents);
                    scheduleDbContext.SaveChanges();
                }

            }
        }
    }
}