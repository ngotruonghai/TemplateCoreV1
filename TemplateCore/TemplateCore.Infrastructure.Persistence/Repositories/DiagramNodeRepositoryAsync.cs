
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DiagramNodeRepositoryAsync : GenericRepositoryAsync<DiagramNode>, IDiagramNodeRepositoryAsync
    {
        private readonly DbSet<DiagramNode> _diagramNodes;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public DiagramNodeRepositoryAsync(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService) : base(dbContext)
        {
            _diagramNodes = dbContext.Set<DiagramNode>();
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<IEnumerable<DiagramNode>> GetDiagramNodeByQuyTrinhId(int QuyTrinhId)
        {
            var diagram = await _diagramNodes.Where(x => x.DanhSachQuyTrinhId == QuyTrinhId && x.UserParentId == _authenticatedUserService.parentUserId).AsQueryable().ToListAsync();

            return diagram;
        }
    }
}
