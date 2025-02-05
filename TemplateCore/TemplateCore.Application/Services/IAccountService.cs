using TemplateCore.Domain.Entities;

namespace TemplateCore.Application.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

        Task<IEnumerable<object>> GetAllAcount();

        Task<object> GetAccountById(string UserId);

        Task<IEnumerable<ApplicationUser>> GetUserThuocMaPhongBan(string MaPhongBan);

        Task<IEnumerable<object>> GetAllRole();

        Task<IEnumerable<object>> GetAllUserPermission();

        Task<string> GetUserNameById(string UserId);
    }
}
