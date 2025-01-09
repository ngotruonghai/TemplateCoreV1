using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCore.Application.Services
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string maphongban { get; }
        string keyactive { get; }
        string parentUserId { get; }
        string typeuser { get; }
        string username { get; }
        string permission { get; }

        /// <summary>
        /// Kiểm tra xem có phải SupperAdmin không
        /// </summary>
        bool CheckUserSupperAdmin {  get; }
    }
}
