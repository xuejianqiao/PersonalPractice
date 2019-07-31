using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWeb.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            //读取配置信息
            AppSettings.Instance.GetConfigurationRoot(env.ContentRootPath,env.EnvironmentName);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                #region 自定义验证策略
                option.AddPolicy("multiClaim", policy => policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim("aud", "roberAudience");
                }));
                option.AddPolicy("common", policy => policy.Requirements.Add(new CommonAuthorize()));
                option.AddPolicy("ageRequire", policy => policy.Requirements.Add(new AgeRequireMent(20)));
                #endregion
            }).AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(option =>
            {
                TokenContext.securityKey = "GQDstclechengroberbojPOXOYg5MbeJ1XT0uFiwDVvVBrk";
                //设置需要验证的项目
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = "roberAudience",//Audience
                    ValidIssuer = "roberIssuer",//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenContext.securityKey))//拿到SecurityKey
                };
               
            });
            //自定义策略IOC添加
            services.AddSingleton<IAuthorizationHandler, CommonAuthorizeHandler>();
            services.AddSingleton<IAuthorizationHandler, AgeRequireHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().AddFluentValidation(o =>
            {
                o.RegisterValidatorsFromAssemblyContaining<BaseRequestValidate>();
            }); ;

            
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
