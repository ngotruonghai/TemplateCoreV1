using TemplateCore.Infrastructure.Shared.Environments;
using TemplateCore.Infrastructure.Shared.Respositories;

namespace TemplateCore.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddEnvironmentVariablesExtension(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseSettingsProvider, DatabaseSettingsProvider>();
        }
    }
}
