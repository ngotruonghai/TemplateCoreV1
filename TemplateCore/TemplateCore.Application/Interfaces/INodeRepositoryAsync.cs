namespace TemplateCore.Application.Interfaces
{
     public interface INodeRepositoryAsync : IGenericRepositoryAsync<Node>
    {
        /// <summary>
        /// Lấy thông tin node theo quytrinhId
        /// </summary>
        /// <param name="QuyTringId"></param>
        /// <returns></returns>
        Task<IEnumerable<Node>> GetNodeByQuyTrinhId(int QuyTringId);
    }
}
