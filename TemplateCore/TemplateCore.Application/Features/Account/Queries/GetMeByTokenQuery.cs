using TemplateCore.Application.Features.Account.Commands;
using TemplateCore.Application.Services;
using TemplateCore.Domain.DTOs.Account;

namespace TemplateCore.Application.Features.Account.Queries
{
    public  class GetMeByTokenQuery: IRequest<Response<GetMeByTokenQueryModel>>
    {

        public class GetMeByTokenQueryHandlre : IRequestHandler<GetMeByTokenQuery, Response<GetMeByTokenQueryModel>>
        {
            private readonly IAccountService _accountService;
            private readonly IAuthenticatedUserService authenticatedUserService;

            public GetMeByTokenQueryHandlre(IAccountService accountService, 
                IAuthenticatedUserService authenticatedUserService)
            {
                _accountService = accountService;
                this.authenticatedUserService = authenticatedUserService;
            }

            public Task<Response<GetMeByTokenQueryModel>> Handle(GetMeByTokenQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
