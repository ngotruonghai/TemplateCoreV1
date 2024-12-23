namespace TemplateCore.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public RoleNamePermission? Permission { get; set; } = null;
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }

    public class Permission
    {
        public string action { get; set; }
        public List<string> resource { get; set; }
    }

    public class RoleNamePermission
    {
        public string role { get; set; }
        public List<Permission> permissions { get; set; }
    }
}
