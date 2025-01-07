using TemplateCore.Application.Features.Account.Queries;

namespace TemplateCore.Application.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

        Task<IEnumerable<object>> GetAllAcount();

        Task<object> GetAccountById(string UserId);
    }
}
