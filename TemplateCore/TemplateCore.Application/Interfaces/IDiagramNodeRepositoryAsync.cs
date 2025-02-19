namespace TemplateCore.Application.Interfaces
{
    public interface IDiagramNodeRepositoryAsync : IGenericRepositoryAsync<DiagramNode>
    {
        public Task<IEnumerable<DiagramNode>> GetDiagramNodeByQuyTrinhId(int QuyTrinhId);
    }
}
