using TemplateCore.Application.Features.Account.Queries;

namespace TemplateCore.Application.Features.Account.Commands
{
    public class AddAccountCommand: IRequest<Response<int>>
    {

        public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, Response<int>>
        {

            public Task<Response<int>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
