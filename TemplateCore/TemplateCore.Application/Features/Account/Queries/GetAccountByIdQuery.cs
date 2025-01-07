
namespace TemplateCore.Application.Features.Account.Queries
{
    public class GetAccountByIdQuery: IRequest<Response<object>>
    {
        public string? UserId {  get; set; }

        public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Response<object>>
        {
            private readonly IAccountService _accountService;
            public GetAccountByIdQueryHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Response<object>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.UserId)) throw new Exception("UserId không được để rỗng");

                    var user = await _accountService.GetAccountById(request.UserId);
                if (user == null) throw new Exception("Không tìm thấy user này");

                return new Response<object>(user);
            }
        }
    }   
}
