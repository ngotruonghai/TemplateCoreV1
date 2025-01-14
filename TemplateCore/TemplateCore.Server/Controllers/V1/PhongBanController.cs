using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.Application.Features.PhongBan.Commands;
using TemplateCore.Application.Features.PhongBan.Queries;
using TemplateCore.Application.Features.QuyTrinhNode.Queries;
namespace TemplateCore.Server.Controllers.V1
{
    [Route("api/quanlythongtin/[controller]")]
    [Authorize]
    public class PhongBanController : BaseApiController
    {
        [Obsolete]
        public PhongBanController() { }

        [HttpPost("AddPhongBan")]
        public async Task<IActionResult> AddPhongBan(AddPhongBanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("DanhSachPhongBan")]
        public async Task<IActionResult> DanhSachPhongBan()
        {
            return Ok(await Mediator.Send(new GetAllPhongBanCommand()));
        }
    }
}
