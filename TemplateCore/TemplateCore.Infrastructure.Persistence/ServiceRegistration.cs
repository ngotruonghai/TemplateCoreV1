using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                b => b.MigrationsAssembly(assembly))
                //.UseLazyLoadingProxies()
                );
            }
        }


        public static void AddPersistenceRepositories(this IServiceCollection services)
        {
            #region Repositories
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IDanhSachQuyTrinhRepositoryAsync, DanhSachQuyTrinhRepositoryAsync>();
            services.AddScoped<IDiagramNodeRepositoryAsync, DiagramNodeRepositoryAsync>();
            services.AddScoped<INodeRepositoryAsync, NodeRepositoryAsync>();
            services.AddScoped<ITransaction, Transaction>();
            services.AddScoped<IPhongBanRepositoryAsync, PhongBanRepositoryAsync>();
            services.AddScoped<INextStepRepositoryAsync, NextStepRepositoryAsync>();
            services.AddScoped<INhanSuTheoDoiQuyTrinhRepositoryAsync, NhanSuTheoDoiQuyTrinhRepositoryAsync>();
            services.AddScoped<IPhongBanTheoDoiQuyTrinhRepositoryAsync, PhongBanTheoDoiQuyTrinhRepositoryAsync>();
            services.AddScoped<INhanSuTiepNhanNodeRepositoRyAsync, NhanSuTiepNhanNodeRepositoRyAsync>();
            services.AddScoped<IPhongBanTiepNhanNodeRepositoryAsync, PhongBanTiepNhanNodeRepositoryAsync>();
            services.AddScoped<INodeSettingRepositoryAsync, NodeSettingRepositoryAsync>();
            services.AddScoped<IThongTinCauHinhRepositoryAsync, ThongTinCauHinhRepositoryAsync>();
            services.AddScoped<IThongTinCauHinhBuocRepositoryAsync, ThongTinCauHinhBuocRepositoryAsync>();
            #endregion
        }
    }
}
