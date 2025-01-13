using TemplateCore.Application.Interfaces;

namespace TemplateCore.Application.Features.PhongBan.Commands
{
    public class AddPhongBanCommand : IRequest<Response<int>>
    {

        public string? MaPhongBan { get; set; }
        public string? TenPhongBan { set; get; }
        public string? GhiChu { get; set; }

        public class AddPhongBanCommandHandler : IRequestHandler<AddPhongBanCommand, Response<int>>
        {
            private readonly IAccountService _accountService;
            private readonly IPhongBanRepositoryAsync _phongBanRepository;

            public AddPhongBanCommandHandler(IAccountService accountService, IPhongBanRepositoryAsync phongBanRepository)
            {
                _accountService = accountService;
                _phongBanRepository = phongBanRepository;
            }

            public async Task<Response<int>> Handle(AddPhongBanCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.MaPhongBan)) throw new Exception("Mã phòng ban không được trống");
                return new Response<int>(1);
            }
        }
    }
}
