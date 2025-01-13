namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class PhongBanRepositoryAsync : GenericRepositoryAsync<PhongBan>,IPhongBanRepositoryAsync
    {
        private readonly DbSet<PhongBan> _phongban;

        public PhongBanRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _phongban = dbContext.Set<PhongBan>();
        }
    }
}
