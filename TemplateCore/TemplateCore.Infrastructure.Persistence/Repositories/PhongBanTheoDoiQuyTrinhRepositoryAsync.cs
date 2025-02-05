
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class PhongBanTheoDoiQuyTrinhRepositoryAsync: GenericRepositoryAsync<PhongBanTheoDoiQuyTrinh>, IPhongBanTheoDoiQuyTrinhRepositoryAsync
    {
        private readonly DbSet<PhongBanTheoDoiQuyTrinh> _phongbantheodoiquytrinh;

        public PhongBanTheoDoiQuyTrinhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _phongbantheodoiquytrinh = dbContext.Set<PhongBanTheoDoiQuyTrinh>();
        }
    }
}
