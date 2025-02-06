namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NodeSettingRepositoryAsync:  GenericRepositoryAsync<NodeSetting>, INodeSettingRepositoryAsync
    {
        private readonly DbSet<NodeSetting> _nodesetting;

        public NodeSettingRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nodesetting = dbContext.Set<NodeSetting>();
        }
    }
}
