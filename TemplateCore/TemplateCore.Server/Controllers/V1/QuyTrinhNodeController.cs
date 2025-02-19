using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.Application.Features.QuyTrinhNode.Commads;
using TemplateCore.Application.Features.QuyTrinhNode.Queries;

namespace TemplateCore.Server.Controllers.V1
{
    [Route("api/quanlythongtin/[controller]")]
    [Authorize]
    public class QuyTrinhNodeController : BaseApiController
    {
        [Obsolete]
        public QuyTrinhNodeController() { }


        /// <summary>
        /// Add quy trình
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("AddQuyTrinhNode")]
        public async Task<IActionResult> AddQuyTrinhNode(AddNodeDetailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// lấy danh sách quy trình thuộc cty
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetDanhSachQuyTinh")]
        public async Task<IActionResult> GetDanhSachQuyTinh()
        {
            return Ok(await Mediator.Send(new GetAllDanhSachQuyTrinh()));
        }

        /// <summary>
        /// Get quy trình by Id (đang chậm)
        /// </summary>
        /// <param name="quytrinhId"></param>
        /// <returns></returns>
        [HttpGet("GetQuyTrinhById")]
        public async Task<IActionResult> GetQuyTrinhById(int quytrinhId)
        {
            return Ok(await Mediator.Send(new GetQuyTrinhByIdQuery() { QuytrinhId = quytrinhId }));
        }

        /// <summary>
        /// Get quy trình by Id (tối ưu hơn)
        /// </summary>
        /// <param name="quytrinhId"></param>
        /// <returns></returns>
        [HttpGet("GetQuyTrinhById_C2")]
        public async Task<IActionResult> GetQuyTrinhById_C2(int quytrinhId)
        {
            return Ok(await Mediator.Send(new GetQuyTrinhById_C2() { QuytrinhId = quytrinhId }));
        }
    }
}
