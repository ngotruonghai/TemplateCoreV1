namespace TemplateCore.Application.Interfaces
{
    public interface IPhongBanRepositoryAsync : IGenericRepositoryAsync<PhongBan>
    {
        Task<IEnumerable<PhongBan>> DanhSachPhongBan();

        Task<PhongBan> GetPhongBanId(int Id);

        public Task<string> GetNamePhongBanById(int phongId);
    }
}
