using System.ComponentModel.DataAnnotations;
using TemplateCore.Application.Features.Account.Commands;

namespace TemplateCore.Application.Features.QuyTrinhNode.Commads
{
    public class AddNodeDetailCommand : IRequest<Response<object>>
    {
       


        public class AddNodeDetailCommandHandler : IRequestHandler<AddNodeDetailCommand, Response<object>>
        {
            public AddNodeDetailCommandHandler() { }

            public async Task<Response<object>> Handle(AddNodeDetailCommand request, CancellationToken cancellationToken)
            {
                return new Response<object>(1);
            }
        }
    }
}
