namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NhanSuTiepNhanNodeRepositoRyAsync: GenericRepositoryAsync<NhanSuTiepNhanNode>,INhanSuTiepNhanNodeRepositoRyAsync
    {
        private readonly DbSet<NhanSuTiepNhanNode> _nhasutiepnhannode;

        public NhanSuTiepNhanNodeRepositoRyAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nhasutiepnhannode = dbContext.Set<NhanSuTiepNhanNode>();
        }
    }
}
