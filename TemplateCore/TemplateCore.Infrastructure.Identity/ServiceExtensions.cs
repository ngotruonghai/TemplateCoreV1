
using MediatR;

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
