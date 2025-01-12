
using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.QuyTrinhNode.Queries
{
    public class GetNodeDetailByIdQuery : IRequest<Response<object>>
    {
        public int QuyTrinhId { get; set; }

        public class GetNodeDetailByIdQueryhandler : IRequestHandler<GetNodeDetailByIdQuery, Response<object>>
        {
            private readonly IDanhSachQuyTrinhRepositoryAsync _danhSachQuyTrinhRepository;

            public GetNodeDetailByIdQueryhandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepository)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepository;
            }

            public async Task<Response<object>> Handle(GetNodeDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var quytrinh = await _danhSachQuyTrinhRepository.GetQuyTrinhId(request.QuyTrinhId);

                return new Response<object>(quytrinh);
            }
        }
    }
}
