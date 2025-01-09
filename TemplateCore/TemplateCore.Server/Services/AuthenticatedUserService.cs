using System.Security.Claims;
using TemplateCore.Application.Interfaces;

namespace TemplateCore.Server.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            maphongban = httpContextAccessor.HttpContext?.User?.FindFirstValue("maphongban");
            keyactive = httpContextAccessor.HttpContext?.User?.FindFirstValue("keyactive");
            parentUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("parentUserId");
            typeuser = httpContextAccessor.HttpContext?.User?.FindFirstValue("typeuser");
        }

        public string UserId { get; }

        public string maphongban { get; }

        public string keyactive { get; }

        public string parentUserId { get; }

        public string typeuser { get; }
    }
}
