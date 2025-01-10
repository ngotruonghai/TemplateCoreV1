namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NodeRepositoryAsync : GenericRepositoryAsync<Node>, INodeRepositoryAsync
    {
        private readonly DbSet<Node> _nodes;

        public NodeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nodes = dbContext.Set<Node>();
        }
    }
}
