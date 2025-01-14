
using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.PhongBan.Queries
{
    public class GetPhongBanIdCommand: IRequest<Response<object>>
    {
        public int Id {  get; set; }

        public class GetPhongBanIdCommandHandler : IRequestHandler<GetPhongBanIdCommand, Response<object>>
        {
            private readonly IPhongBanRepositoryAsync _phongBanRepositoryAsync;

            public GetPhongBanIdCommandHandler(IPhongBanRepositoryAsync phongBanRepositoryAsync)
            {
                _phongBanRepositoryAsync = phongBanRepositoryAsync;
            }

            public async Task<Response<object>> Handle(GetPhongBanIdCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null) throw new Exception("PhongBanId not found");

                var phongban = _phongBanRepositoryAsync.GetPhongBanId(request.Id);

                if (phongban == null) throw new Exception("Không tồn tại phòng ban này");

                return new Response<object>(phongban);
            }
        }
    }
}
