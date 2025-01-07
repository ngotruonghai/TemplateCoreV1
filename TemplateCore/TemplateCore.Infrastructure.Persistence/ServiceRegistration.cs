using Microsoft.Extensions.DependencyInjection;
using TemplateCore.Infrastructure.Persistence.Contexts;
using TemplateCore.Infrastructure.Shared.Environments;

namespace TemplateCore.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddSqlServerPersistenceInfrastructure(this IServiceCollection services, string assembly)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var _dbSetting = scope.ServiceProvider.GetRequiredService<IDatabaseSettingsProvider>();
                string appConnStr = _dbSetting.GetSQLServerConnectionString();
                services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
                options
                //.AddInterceptors(serviceProvider.GetRequiredService<StatusUpdateInterceptor>())
                .UseSqlServer(
                appConnStr,
                b => b.MigrationsAssembly(assembly)
                ));
            }
        }


        public static void AddPersistenceRepositories(this IServiceCollection services)
        {
           
        }
    }
}
