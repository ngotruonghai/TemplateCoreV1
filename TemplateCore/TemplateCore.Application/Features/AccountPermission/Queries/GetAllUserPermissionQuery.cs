using TemplateCore.Application.Features.Account.Queries;
using IAuthenticatedUserService = TemplateCore.Application.Interfaces.IAuthenticatedUserService;

namespace TemplateCore.Application.Features.AccountPermission.Queries
{
    public class GetAllUserPermissionQuery : IRequest<Response<object>>
    {

        public class GetAllUserPermissionQueryhandler : IRequestHandler<GetAllUserPermissionQuery, Response<object>>
        {
            private readonly IAccountService _accountService;
            private readonly IAuthenticatedUserService _authenticatedUserService;

            public GetAllUserPermissionQueryhandler(IAccountService accountService
                , IAuthenticatedUserService authenticatedUserService)
            {
                _accountService = accountService;
                _authenticatedUserService = authenticatedUserService;
            }

            public async Task<Response<object>> Handle(GetAllUserPermissionQuery request, CancellationToken cancellationToken)
            {
                return new Response<object>(1);
            }
        }
    }
}
