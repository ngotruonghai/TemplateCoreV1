
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NodeRepositoryAsync : GenericRepositoryAsync<Node>, INodeRepositoryAsync
    {
        private readonly DbSet<Node> _nodes;

        public NodeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nodes = dbContext.Set<Node>();
        }

        public async Task<IEnumerable<Node>> GetNodeByQuyTrinhId(int QuyTringId)
        {
            var node = await _nodes.Where(x => x.DanhSachQuyTrinhId == QuyTringId).ToListAsync();
            return node;
        }
    }
}
