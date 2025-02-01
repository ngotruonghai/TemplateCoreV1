using TemplateCore.Domain.DTOs.Account;

namespace TemplateCore.Application.Features.Account.Queries
{
    public class GetAllAccountQuery: IRequest<Response<object>>
    {

        public class GetAllAccountQueryHandler : IRequestHandler<GetAllAccountQuery, Response<object>>
        {
            private readonly IAccountService _accountService;

            public GetAllAccountQueryHandler(IAccountService accountService) {
                _accountService = accountService;
            }

            public async Task<Response<object>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
            {
                var data = await _accountService.GetAllUserPermission();

                return new Response<object>(data);
            }
        }
    }
}
