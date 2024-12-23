namespace TemplateCore.Application.Features.Account.Commands
{
    public class AccountCommand : IRequest<Response<object>>
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string ipAddress {  get; set; }

        public class AccountCommandHandler : IRequestHandler<AccountCommand, Response<object>>
        {
            private readonly IAccountService _accountService;

            public AccountCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<Response<object>> Handle(AccountCommand request, CancellationToken cancellationToken)
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Password = request.Password
                };
                var user =  await _accountService.AuthenticateAsync(authenticationRequest,request.ipAddress);

                return new Response<object>(user);
            }
        }
    }
}
