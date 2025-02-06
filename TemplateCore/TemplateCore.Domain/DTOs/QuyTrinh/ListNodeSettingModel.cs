namespace TemplateCore.Domain.DTOs.QuyTrinh
{
    public class ListNodeSettingModel
    {
        public string? KeyNode { get; set; }

        public string? GhiChu {  get; set; }

        public bool IsTaoTaskBaoCao { get; set; } = false;

        public bool IsGuiMailPhongBanTiepNhan { get; set; } = false;

        public bool IsGuiMailNhacNho { get; set; } = false;

        public int? CauHinhMailNhacNho { get; set; }

        public ICollection<string>? nhanSuNodeModels { get; set; }
        public ICollection<int>? PhongBanNodeModels { get; set; }
    }
}
