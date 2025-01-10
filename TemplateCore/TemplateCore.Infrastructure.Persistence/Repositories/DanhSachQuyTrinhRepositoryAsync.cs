namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DanhSachQuyTrinhRepositoryAsync : GenericRepositoryAsync<DanhSachQuyTrinh>, IDanhSachQuyTrinhRepositoryAsync
    {
        private readonly DbSet<DanhSachQuyTrinh> _danhSachQuyTrinhs;

        public DanhSachQuyTrinhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _danhSachQuyTrinhs = dbContext.Set<DanhSachQuyTrinh>();
        }
    }
}
