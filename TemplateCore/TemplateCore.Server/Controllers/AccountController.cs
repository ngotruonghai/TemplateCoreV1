using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.Application.DTOs.Account;
using TemplateCore.Application.Features.Account.Commands;
using TemplateCore.Application.Features.Account.Queries;
using TemplateCore.Infrastructure.Identity.Features.Users.Command;

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

        /// <summary>
        /// Danh sách User
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await Mediator.Send(new GetAllAccountQuery()));
        }

        /// <summary>
        /// Lấy thông tin User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return Ok(await Mediator.Send(new GetAccountByIdQuery() { UserId = userId }));
        }

        /// <summary>
        /// Tạo Account hệ thống theo phòng ban
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateAcount")]
        public async Task<IActionResult> CreateAcount(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Danh sách Role hệ thống
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetRole")]
        public async Task<IActionResult> GetRole()
        {
            return Ok(await Mediator.Send(new GetRoleQuery()));
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
