using Microsoft.AspNetCore.Http;

namespace TemplateCore.Infrastructure.Identity.Repositories
{
    public class AuthenticatedUserService: IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
    }
}
