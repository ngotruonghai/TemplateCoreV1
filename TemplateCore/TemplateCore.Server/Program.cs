
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using TemplateCore.Infrastructure.Identity;
using TemplateCore.Infrastructure.Identity.Contexts;
using TemplateCore.Infrastructure.Identity.Models;
using TemplateCore.Server.Extensions;
using TemplateCore.Server.Initializer;

namespace TemplateCore.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var _services = builder.Services;
            var _config = builder.Configuration;
            var _env = builder.Environment;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            _services.AddEnvironmentVariablesExtension();

            #region Swagger

            _services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region Add Identity
            _services.AddSqlServerIdentityInfrastructure(typeof(Program).Assembly.FullName);
            _services.AddIdentityLayer();
            _services.AddIdentityRepositories(_config);
            _services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<IdentityContext>()
  .AddDefaultTokenProviders();

            #endregion



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var initializer = new ApplicationInitializer(scope.ServiceProvider, configuration, webHostEnvironment);
                await initializer.InitializeAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
