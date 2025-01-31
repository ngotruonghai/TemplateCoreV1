using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateCore.Server.Controllers.V1
{
    [Route("api/accountpermission/[controller]")]
    //[Authorize]
    public class AccountPermissionController: BaseApiController
    {
        [Obsolete]
        public AccountPermissionController() { }

        [HttpGet]
        public async Task<IActionResult> DanhSachUserPhanQuyen()
        {
            return Ok("123");
        }
    }
}
