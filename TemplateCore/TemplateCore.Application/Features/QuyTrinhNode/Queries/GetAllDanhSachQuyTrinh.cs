using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.QuyTrinhNode.Queries
{
    public class GetAllDanhSachQuyTrinh : IRequest<Response<object>>
    {

        public class GetAllDanhSachQuyTrinhHandler : IRequestHandler<GetAllDanhSachQuyTrinh, Response<object>>
        {
            private readonly IDanhSachQuyTrinhRepositoryAsync _danhSachQuyTrinhRepository;

            public GetAllDanhSachQuyTrinhHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepository)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepository;
            }

            public async Task<Response<object>> Handle(GetAllDanhSachQuyTrinh request, CancellationToken cancellationToken)
            {
                var quytrinh = await _danhSachQuyTrinhRepository.GetAllDanhSachQuyTrinh();

                return new Response<object>(quytrinh);
            }
        }

    }
}
