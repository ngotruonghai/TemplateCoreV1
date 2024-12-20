
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            _services.AddEnvironmentVariablesExtension();

            #region Add Identity
            _services.AddSqlServerIdentityInfrastructure(typeof(Program).Assembly.FullName);
            _services.AddIdentityLayer();
            //_services.AddIdentityRepositories(_config);
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
