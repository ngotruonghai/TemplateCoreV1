namespace TemplateCore.Application.Interfaces
{
    public interface IThongTinCauHinhRepositoryAsync : IGenericRepositoryAsync<ThongTinCauHinh>
    {
        /// <summary>
        /// Lấy thông tin cấu hình của quy trình
        /// </summary>
        /// <param name="quytrinhId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ThongTinCauHinh>> GetThongTinCauHinhByQuyTrinhId(int quytrinhId);
    }
}
