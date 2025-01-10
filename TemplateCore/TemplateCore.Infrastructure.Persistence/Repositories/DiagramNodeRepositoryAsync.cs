namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DiagramNodeRepositoryAsync : GenericRepositoryAsync<DiagramNode>, IDiagramNodeRepositoryAsync
    {
        private readonly DbSet<DiagramNode> _diagramNodes;

        public DiagramNodeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _diagramNodes = dbContext.Set<DiagramNode>();
        }
    }
}
