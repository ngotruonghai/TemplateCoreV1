namespace TemplateCore.Application.Interfaces
{
    public interface INodeSettingRepositoryAsync : IGenericRepositoryAsync<NodeSetting>
    {
        Task<IEnumerable<NodeSetting>> GetNodeSettingByNodeId(int nodeid);
    }
}
