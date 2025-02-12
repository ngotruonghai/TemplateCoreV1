namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class ThongTinCauHinhRepositoryAsync: GenericRepositoryAsync<ThongTinCauHinh>, IThongTinCauHinhRepositoryAsync
    {
        private readonly DbSet<ThongTinCauHinh> _thongtincauhinh;

        public ThongTinCauHinhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _thongtincauhinh = dbContext.Set<ThongTinCauHinh>();
        }
    }
}
