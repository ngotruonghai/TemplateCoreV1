using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.Application.Features.Account.Queries;
using TemplateCore.Application.Features.QuyTrinhNode.Commads;

namespace TemplateCore.Server.Controllers.V1
{
    [Route("api/quanlythongtin/[controller]")]
    public class QuyTrinhNodeController : BaseApiController
    {
        [Obsolete]
        public QuyTrinhNodeController() { }


        
        [HttpPost("AddQuyTrinhNode")]
        public async Task<IActionResult> AddQuyTrinhNode(AddNodeDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
