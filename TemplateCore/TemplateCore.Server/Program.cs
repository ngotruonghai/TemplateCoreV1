using Microsoft.OpenApi.Models;
using System.Reflection;
using TemplateCore.Infrastructure.Identity;
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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            _services.AddEnvironmentVariablesExtension();

            #region Swagger

            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - TemplateCore.WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "codewithmukesh",
                        Email = "hello@codewithmukesh.com",
                        Url = new Uri("https://codewithmukesh.com/contact"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region Add Identity

            _services.AddSqlServerIdentityInfrastructure(typeof(Program).Assembly.FullName);
            _services.AddIdentityRepositories(_config);
            _services.AddIdentityLayer();
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

            app.UseErrorHandlingMiddleware();

            app.MapControllers();

            app.UseCors(policy =>policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


            app.Run();
        }
    }
}
