namespace TemplateCore.Application.Interfaces
{
    public interface IDanhSachQuyTrinhRepositoryAsync: IGenericRepositoryAsync<DanhSachQuyTrinh>
    {
        Task<IEnumerable<DanhSachQuyTrinh>> GetQuyTrinhId(int QuyTrinhId);

        Task<IEnumerable<DanhSachQuyTrinh>> GetAllDanhSachQuyTrinh();

        /// <summary>
        /// Chỉ đọc và không chỉnh sửa
        /// </summary>
        /// <param name="QuyTrinhId"></param>
        /// <returns></returns>
        Task<DanhSachQuyTrinh> GetDanhSachQuyTrinhByid(int QuyTrinhId);
    }
}
