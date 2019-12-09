using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UpdateNotator.Application.ApplicationServices;
using UpdateNotator.Application.ApplicationServices.Entry;
using UpdateNotator.Application.ApplicationServices.Topic;
using UpdateNotator.Application.ApplicationServices.User;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;
using UpdateNotator.Infrasructure.Data;
using UpdateNotator.Web.Auth;
using UpdateNotator.Web.Middlewares;

namespace UpdateNotator.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region Auth
            const string signingSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
            var signingKey = new SignInSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            const string jwtSchemeName = "JwtBearer";
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtSchemeName;
                options.DefaultChallengeScheme = jwtSchemeName;
            }).AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingDecodingKey.GetKey(),

                    ValidateIssuer = true,
                    ValidIssuer = "Unotator",

                    ValidateAudience = true,
                    ValidAudience = "UnotatorClient",

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });
            #endregion

            #region DB Context
            string connectionString = Configuration.GetConnectionString("AppDb");
            services.AddDbContext<Infrasructure.Data.AppContext>(m => m.UseSqlServer(connectionString));
            #endregion

            #region Infrastructure
            services.AddTransient<IRepository<User>, EFRepository<User>>();
            services.AddTransient<IRepository<Topic>, EFRepository<Topic>>();
            services.AddTransient<IRepository<Entry>, EFRepository<Entry>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Application Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IEntryService, EntryService>();
            #endregion

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Map());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
