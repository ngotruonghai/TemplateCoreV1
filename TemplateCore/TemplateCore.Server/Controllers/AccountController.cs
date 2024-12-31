using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.Application.DTOs.Account;
using TemplateCore.Application.Features.Account.Commands;
using TemplateCore.Application.Features.Account.Queries;

namespace TemplateCore.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseApiController
    {

        [Obsolete]
        public AccountController()
        {

        }

        [HttpPost("AuthenticateAsync")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            return Ok(await Mediator.Send(new AccountCommand()
            {
                UserName = authenticationRequest.UserName,
                Password = authenticationRequest.Password,
                Email = authenticationRequest.Email,
                ipAddress = GenerateIPAddress()
            }));
        }

        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await Mediator.Send(new GetAllAccountQuery()));
        }

        #region function

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        #endregion
    }
}
