namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListNodeSettingModel
    {
        public string? KeyNode { get; set; }

        public ICollection<NhanSuNodeModel> nhanSuNodeModels { get; set; }
        public ICollection<PhongBanNodeModel> PhongBanNodeModels { get; set; }
    }
}
