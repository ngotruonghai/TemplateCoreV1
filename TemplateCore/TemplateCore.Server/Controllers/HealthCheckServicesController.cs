using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TemplateCore.Server.Controllers
{
    [Route("api/quanlythongtin/[controller]")]
    [Authorize]
    public class HealthCheckServicesController : BaseApiController
    {
        [Obsolete]
        public HealthCheckServicesController() { }

        [HttpGet("HealthCheckAuthen")]
        public async Task<IActionResult> HealthCheckAuthen()
        {
            return Ok(HttpStatusCode.OK);
        }
    }
}
