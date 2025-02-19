
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NodeSettingRepositoryAsync:  GenericRepositoryAsync<NodeSetting>, INodeSettingRepositoryAsync
    {
        private readonly DbSet<NodeSetting> _nodesetting;

        public NodeSettingRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nodesetting = dbContext.Set<NodeSetting>();
        }

        public async Task<IEnumerable<NodeSetting>> GetNodeSettingByNodeId(int nodeid)
        {
            var nodestting = await _nodesetting.Where(x => x.NodeId == nodeid)
                                    .Include(x => x.NhanSuTiepNhanNodes)
                                    .Include(x=> x.PhongBanTiepNhanNodes)
                                    .AsQueryable()
                                    .ToListAsync();
            return nodestting;
        }
    }
}
