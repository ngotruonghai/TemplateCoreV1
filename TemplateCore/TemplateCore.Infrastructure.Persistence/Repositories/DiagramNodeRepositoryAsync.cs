
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DiagramNodeRepositoryAsync : GenericRepositoryAsync<DiagramNode>, IDiagramNodeRepositoryAsync
    {
        private readonly DbSet<DiagramNode> _diagramNodes;

        public DiagramNodeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _diagramNodes = dbContext.Set<DiagramNode>();
        }

        public async Task<IEnumerable<DiagramNode>> GetDiagramNodeByQuyTrinhId(int QuyTrinhId)
        {
            var diagram = await _diagramNodes.Where(x => x.DanhSachQuyTrinhId == QuyTrinhId).ToListAsync();

            return diagram;
        }
    }
}
