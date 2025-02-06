namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class PhongBanTiepNhanNodeRepositoryAsync: GenericRepositoryAsync<PhongBanTiepNhanNode>, IPhongBanTiepNhanNodeRepositoryAsync
    {
        private readonly DbSet<PhongBanTiepNhanNode> _phongbantiepnhannode;

        public PhongBanTiepNhanNodeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _phongbantiepnhannode = dbContext.Set<PhongBanTiepNhanNode>();
        }
    }
}
