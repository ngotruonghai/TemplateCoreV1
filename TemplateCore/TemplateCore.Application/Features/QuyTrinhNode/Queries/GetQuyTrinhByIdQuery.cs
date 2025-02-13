using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.QuyTrinhNode.Queries
{
    public class GetQuyTrinhByIdQuery : IRequest<Response<object>>
    {
        public int QuytrinhId {  get; set; }

        public class GetQuyTrinhByIdQueryHandler : IRequestHandler<GetQuyTrinhByIdQuery, Response<object>>
        {
            #region RepositoryAsync
            private readonly IDanhSachQuyTrinhRepositoryAsync _danhSachQuyTrinhRepository;
            #endregion

            public GetQuyTrinhByIdQueryHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepositoryAsync;
            }

            public async Task<Response<object>> Handle(GetQuyTrinhByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var quytrinh = await _danhSachQuyTrinhRepository.GetDanhSachQuyTrinhByid(request.QuytrinhId);
                    
                    return new Response<object>(quytrinh);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
