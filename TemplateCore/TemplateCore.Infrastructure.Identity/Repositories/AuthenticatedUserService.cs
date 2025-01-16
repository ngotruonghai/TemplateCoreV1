using Microsoft.AspNetCore.Http;

namespace TemplateCore.Infrastructure.Identity.Repositories
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid") ?? "";
            maphongban = httpContextAccessor.HttpContext?.User?.FindFirstValue("maphongban") ?? "";
            keyactive = httpContextAccessor.HttpContext?.User?.FindFirstValue("keyactive") ?? "";
            parentUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("parentUserId") ?? "";
            typeuser = httpContextAccessor.HttpContext?.User?.FindFirstValue("typeuser") ?? "";
            username = httpContextAccessor.HttpContext?.User?.FindFirstValue("username") ?? "";
            permission = httpContextAccessor.HttpContext?.User?.FindFirstValue("permission") ?? "";
            CheckUserSupperAdmin = this.CheckTokenUserSupperAdmin();
        }

        public string UserId { get; }

        public string maphongban { get; }

        public string keyactive { get; }

        public string parentUserId { get; }

        public string typeuser { get; }
        public string username { get; }

        public bool CheckUserSupperAdmin { get; }

        public string permission { get; }

        #region function
        private bool CheckTokenUserSupperAdmin()
        {
            try
            {
                if (this.permission == "SuperAdmin" && this.parentUserId == "1")
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }

        }

        #endregion
    }
}
