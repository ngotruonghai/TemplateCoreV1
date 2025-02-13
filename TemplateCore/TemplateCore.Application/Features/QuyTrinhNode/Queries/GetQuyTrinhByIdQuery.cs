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
            private readonly IDiagramNodeRepositoryAsync _diagramNodeRepository;
            private readonly INodeRepositoryAsync _nodeRepositoryAsync;
            private readonly IMapper _mapper;
            private readonly Interfaces.IAuthenticatedUserService _authenticatedUserService;
            private readonly ITransaction _transaction;
            private readonly INextStepRepositoryAsync _nextStepRepositoryAsync;
            private readonly IAccountService _accountService;
            private readonly INhanSuTheoDoiQuyTrinhRepositoryAsync _nhanSuTheoDoiQuyTrinhRepository;
            private readonly IPhongBanTheoDoiQuyTrinhRepositoryAsync _phongBanTheoDoiQuyTrinhRepository;
            private readonly IPhongBanRepositoryAsync _phongBanRepository;
            private readonly INhanSuTiepNhanNodeRepositoRyAsync _nhanSuTiepNhanNodeRepository;
            private readonly IPhongBanTiepNhanNodeRepositoryAsync _phongBanTiepNhanNodeRepository;
            private readonly INodeSettingRepositoryAsync _nodeSettingRepository;
            private readonly IThongTinCauHinhBuocRepositoryAsync _thongtincauhinhbuocrepository;
            private readonly IThongTinCauHinhRepositoryAsync _thongtincauhinhRepository;
            #endregion

            public GetQuyTrinhByIdQueryHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync,
               IDiagramNodeRepositoryAsync diagramNodeRepository,
               INodeRepositoryAsync nodeRepositoryAsync,
               IMapper mapper,
               Interfaces.IAuthenticatedUserService authenticatedUserService,
               ITransaction transaction,
               INextStepRepositoryAsync nextStepRepositoryAsync,
               IAccountService accountService,
               INhanSuTheoDoiQuyTrinhRepositoryAsync nhanSuTheoDoiQuyTrinhRepository,
               IPhongBanTheoDoiQuyTrinhRepositoryAsync phongBanTheoDoiQuyTrinhRepository,
               IPhongBanRepositoryAsync phongBanRepository,
               INhanSuTiepNhanNodeRepositoRyAsync nhanSuTiepNhanNodeRepository,
               IPhongBanTiepNhanNodeRepositoryAsync phongBanTiepNhanNodeRepository,
               INodeSettingRepositoryAsync nodeSettingRepository,
               IThongTinCauHinhBuocRepositoryAsync thongtincauhinhbuocrepository,
               IThongTinCauHinhRepositoryAsync thongtincauhinhRepository)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepositoryAsync;
                _diagramNodeRepository = diagramNodeRepository;
                _nodeRepositoryAsync = nodeRepositoryAsync;
                _mapper = mapper;
                _authenticatedUserService = authenticatedUserService;
                _transaction = transaction;
                _nextStepRepositoryAsync = nextStepRepositoryAsync;
                _accountService = accountService;
                _nhanSuTheoDoiQuyTrinhRepository = nhanSuTheoDoiQuyTrinhRepository;
                _phongBanTheoDoiQuyTrinhRepository = phongBanTheoDoiQuyTrinhRepository;
                _phongBanRepository = phongBanRepository;
                _nhanSuTiepNhanNodeRepository = nhanSuTiepNhanNodeRepository;
                _phongBanTiepNhanNodeRepository = phongBanTiepNhanNodeRepository;
                _nodeSettingRepository = nodeSettingRepository;
                _thongtincauhinhbuocrepository = thongtincauhinhbuocrepository;
                _thongtincauhinhRepository = thongtincauhinhRepository;
            }

            public async Task<Response<object>> Handle(GetQuyTrinhByIdQuery request, CancellationToken cancellationToken)
            {
                var quytrinh = await _danhSachQuyTrinhRepository.GetDanhSachQuyTrinhByid(request.QuytrinhId);
                object response = new 
                {
                    dsdsa = "123456"
                };
                return new Response<object>(quytrinh);
            }
        }
    }
}
