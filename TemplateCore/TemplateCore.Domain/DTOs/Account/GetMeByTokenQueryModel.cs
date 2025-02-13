namespace TemplateCore.Domain.DTOs.Account
{
    public class GetMeByTokenQueryModel
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string[] Roles { get; set; } = [];
        public string Uid { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string AvatarUid { get; set; } = string.Empty;
    }
}
