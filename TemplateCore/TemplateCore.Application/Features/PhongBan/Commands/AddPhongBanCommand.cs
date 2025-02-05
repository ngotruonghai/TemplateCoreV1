using SimpleResults;
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
            private readonly Interfaces.IAuthenticatedUserService _authenticatedUserService;
            private readonly IPhongBanRepositoryAsync _phongBanRepository;

            public AddPhongBanCommandHandler(Interfaces.IAuthenticatedUserService authenticatedUserService, IPhongBanRepositoryAsync phongBanRepository)
            {
                _authenticatedUserService = authenticatedUserService;
                _phongBanRepository = phongBanRepository;
            }

            public async Task<Response<int>> Handle(AddPhongBanCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (string.IsNullOrEmpty(request.MaPhongBan)) throw new Exception("Mã phòng ban không được trống");

                    await _phongBanRepository.AddAsync(new Domain.Entities.PhongBan.PhongBan()
                    {
                        TenPhongBan = request.TenPhongBan,
                        MaPhongBan = request.MaPhongBan,
                        GhiChu = request.GhiChu
                    });
                    await _phongBanRepository.SaveChangesAsync();

                    return new Response<int>(1);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    throw;
                }
            }
        }
    }
}
