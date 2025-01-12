namespace TemplateCore.Application.Interfaces
{
    public interface IDanhSachQuyTrinhRepositoryAsync: IGenericRepositoryAsync<DanhSachQuyTrinh>
    {
        Task<IEnumerable<DanhSachQuyTrinh>> GetQuyTrinhId(int QuyTrinhId);
    }
}
