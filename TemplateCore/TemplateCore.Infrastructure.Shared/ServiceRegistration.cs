using Microsoft.AspNetCore.Identity;
using TemplateCore.Domain.Entities;

namespace TemplateCore.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddTransient<IDateTimeService, DateTimeService>();

        }

    }
}
