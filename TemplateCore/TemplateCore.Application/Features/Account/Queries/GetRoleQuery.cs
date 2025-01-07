using TemplateCore.Domain.DTOs.Account;

namespace TemplateCore.Application.Features.Account.Queries
{
    public class GetRoleQuery : IRequest<Response<object>>
    {

        public class GetRoleQueryhandler : IRequestHandler<GetRoleQuery, Response<object>>
        {
            private readonly IAccountService _accountService;

            public GetRoleQueryhandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public  async Task<Response<object>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
            {
                return new Response<object>(await _accountService.GetAllRole());
            }
        }
    }
}
