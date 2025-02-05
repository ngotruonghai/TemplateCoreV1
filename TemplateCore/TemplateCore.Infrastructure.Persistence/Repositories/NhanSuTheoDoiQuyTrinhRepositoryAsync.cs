namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NhanSuTheoDoiQuyTrinhRepositoryAsync: GenericRepositoryAsync<NhanSuTheoDoiQuyTrinh>, INhanSuTheoDoiQuyTrinhRepositoryAsync
    {
        private readonly DbSet<NhanSuTheoDoiQuyTrinh> _nhansutheodoiquytrinh;

        public NhanSuTheoDoiQuyTrinhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nhansutheodoiquytrinh = dbContext.Set<NhanSuTheoDoiQuyTrinh>();
        }
    }
}
