using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.PhongBan.Queries
{
    public class GetAllPhongBanCommand: IRequest<Response<object>>
    {

        public class GetAllPhongBanCommandHandler : IRequestHandler<GetAllPhongBanCommand, Response<object>>
        {
            private readonly IPhongBanRepositoryAsync _phongBanRepositoryAsync;

            public GetAllPhongBanCommandHandler(IPhongBanRepositoryAsync phongBanRepositoryAsync)
            {
                _phongBanRepositoryAsync = phongBanRepositoryAsync;
            }

            public async Task<Response<object>> Handle(GetAllPhongBanCommand request, CancellationToken cancellationToken)
            {
                var phongban = await _phongBanRepositoryAsync.DanhSachPhongBan();

                return new Response<object>(phongban);
            }
        }
    }
}
