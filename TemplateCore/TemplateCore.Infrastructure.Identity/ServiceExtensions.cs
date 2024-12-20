using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TemplateCore.Application.Wrappers;
using TemplateCore.Domain.Settings;

namespace TemplateCore.Infrastructure.Identity
{
    public static class ServiceExtensions
    {
        public static void AddSqlServerIdentityInfrastructure(this IServiceCollection services, string assembly)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var _dbSetting = scope.ServiceProvider.GetRequiredService<IDatabaseSettingsProvider>();
                string appConnStr = _dbSetting.GetSQLServerConnectionString();
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(appConnStr,
                    b =>
                    {
                        b.MigrationsAssembly(assembly);
                    }
                )
                );
            }
        }

        public static void AddIdentityRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            #region Services
            //services.AddTransient<IAccountService, AccountService>();
            #endregion
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(o =>
            //    {
            //        o.RequireHttpsMetadata = false;
            //        o.SaveToken = false;
            //        o.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.Zero,
            //            ValidIssuer = configuration["JWTSettings:Issuer"],
            //            ValidAudience = configuration["JWTSettings:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            //        };
            //        o.Events = new JwtBearerEvents()
            //        {
            //            OnAuthenticationFailed = c =>
            //            {
            //                c.NoResult();
            //                c.Response.StatusCode = 500;
            //                c.Response.ContentType = "text/plain";
            //                return c.Response.WriteAsync(c.Exception.ToString());
            //            },
            //            OnChallenge = context =>
            //            {
            //                context.HandleResponse();
            //                context.Response.StatusCode = 401;
            //                context.Response.ContentType = "application/json";
            //                var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
            //                return context.Response.WriteAsync(result);
            //            },
            //            OnForbidden = context =>
            //            {
            //                context.Response.StatusCode = 403;
            //                context.Response.ContentType = "application/json";
            //                var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
            //                return context.Response.WriteAsync(result);
            //            },
            //        };
            //    });
        }

        public static void AddIdentityLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
