using Microsoft.EntityFrameworkCore;
using TemplateCore.Infrastructure.Identity.Contexts;

namespace TemplateCore.Server.Initializer
{
    public class ApplicationInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private static IWebHostEnvironment _webHostEnvironment;
        private static IConfiguration _configuration;
        private bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;
        private static string ServiceName;

        public ApplicationInitializer(IServiceProvider serviceProvider, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _webHostEnvironment = webHostEnvironment;
            ServiceName = _configuration["ServiceName"] ?? "templatecore";
            if (isProduction)
            {
                ServiceName = Environment.GetEnvironmentVariable("SERVICE_NAME") ?? "templatecore";
            }
        }
        public async Task InitializeAsync()
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            try
            {
                var identityDbContext = _serviceProvider.GetRequiredService<IdentityContext>();
                identityDbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
