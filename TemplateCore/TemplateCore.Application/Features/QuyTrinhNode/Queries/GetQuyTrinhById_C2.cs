using TemplateCore.Domain.DTOs.QuyTrinh;
using IAuthenticatedUserService = TemplateCore.Application.Interfaces.IAuthenticatedUserService;

namespace TemplateCore.Application.Features.QuyTrinhNode.Queries
{
    public class GetQuyTrinhById_C2 : IRequest<Response<object>>
    {
        public int QuytrinhId { get; set; }

        public class GetQuyTrinhById_C2Handler : IRequestHandler<GetQuyTrinhById_C2, Response<object>>
        {

            #region RepositoryAsync
            private readonly IDanhSachQuyTrinhRepositoryAsync _danhSachQuyTrinhRepository;
            private readonly IDiagramNodeRepositoryAsync _diagramNodeRepository;
            private readonly INodeRepositoryAsync _nodeRepositoryAsync;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUserService _authenticatedUserService;
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

            public GetQuyTrinhById_C2Handler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync,
                IDiagramNodeRepositoryAsync diagramNodeRepository,
                INodeRepositoryAsync nodeRepositoryAsync,
                IMapper mapper,
                IAuthenticatedUserService authenticatedUserService,
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

            public async Task<Response<object>> Handle(GetQuyTrinhById_C2 request, CancellationToken cancellationToken)
            {
                try
                {
                    List<ListNodeModel>? NodeMapModels;
                    List<ListDiagramModel>? DiagramNodeModels;
                    List<NextStepModel>? NextStepNodeModels;
                    List<string>? NhanSuIds = new List<string>();
                    List<int>? PhongBanIds = new List<int>();
                    List<ListNodeSettingModel>? NodeSttings=new List<ListNodeSettingModel>();
                    List<ListCauHingThongTinModel>? CauHinhThongTins = new List<ListCauHingThongTinModel>();

                    var quytrinh = await _danhSachQuyTrinhRepository.GetQuyTrinhId(request.QuytrinhId);
                    if (quytrinh == null) throw new Exception("Quy trình không tồn tại");

                    var node = await _nodeRepositoryAsync.GetNodeByQuyTrinhId(request.QuytrinhId);
                    var diagramNode = await _diagramNodeRepository.GetDiagramNodeByQuyTrinhId(request.QuytrinhId);

                    foreach (var inode in node)
                    {
                        var nodesetting = await _nodeSettingRepository.GetNodeSettingByNodeId(inode.Id);
                        string[] nhansutipenhannode = null;
                        int[] phongbantipenhannode = null;

                        foreach (var inodesetting in nodesetting)
                        {
                            nhansutipenhannode = inodesetting.NhanSuTiepNhanNodes.Select(x => x.UserId.ToString()).ToArray();
                            phongbantipenhannode = inodesetting.PhongBanTiepNhanNodes.Select(x => x.PhongbanId).ToArray();
                            NodeSttings.Add(new ListNodeSettingModel()
                            {
                                GhiChu = inodesetting.GhiChu,
                                CauHinhMailNhacNho = inodesetting.CauHinhMailNhacNho,
                                IsGuiMailNhacNho= inodesetting.IsGuiMailNhacNho,
                                IsGuiMailPhongBanTiepNhan = inodesetting.IsGuiMailPhongBanTiepNhan,
                                IsTaoTaskBaoCao = inodesetting.IsTaoTaskBaoCao,
                                nhanSuNodeModels = nhansutipenhannode,
                                PhongBanNodeModels = phongbantipenhannode,
                                KeyNode = inode.KeyId,
                            });
                        }
                    }
                    var nextstep = await _nextStepRepositoryAsync.GetbextStepBtQuyTrinhId(request.QuytrinhId);
                    var thongtincauhinh = await _thongtincauhinhRepository.GetThongTinCauHinhByQuyTrinhId(request.QuytrinhId);

                    foreach(var ithongtincauhinh in thongtincauhinh)
                    {
                        CauHinhThongTins.Add(new ListCauHingThongTinModel()
                        {
                            Index = ithongtincauhinh.Index,
                            IsBatBuocnhap = ithongtincauhinh.IsBatBuocnhap,
                            IsFileDinhKem = ithongtincauhinh.IsFileDinhKem,
                            KichThuocKyTu = ithongtincauhinh.KichThuocKyTu,
                            LoaiThongTin = ithongtincauhinh.LoaiThongTin,
                            NoiDung = ithongtincauhinh.NoiDung,
                            TenThonTin = ithongtincauhinh.TenThonTin,
                            ThongBao = ithongtincauhinh.ThongBao,
                            DanhSachCauHinhBuoc = ithongtincauhinh.ThongTinCauHinhBuocs.Select(x => x.KeyNode).ToArray()
                        });
                    }

                    if(quytrinh.NhanSuTheoDoiQuyTrinhs != null)
                    {
                        NhanSuIds = quytrinh.NhanSuTheoDoiQuyTrinhs.Select(x => x.UserId).ToList();
                    }
                    if (quytrinh.PhongBanTheoDoiQuyTrinhs != null)
                    {
                        PhongBanIds = quytrinh.PhongBanTheoDoiQuyTrinhs.Select(x => x.PhongbanId).ToList();
                    }


                    object obj = new
                    {
                        MaQuyTrinh = quytrinh.MaQuyTrinh,
                        TenQuyTrinh = quytrinh.TenQuyTrinh,
                        NoiDung = quytrinh.NoiDung,
                        ThietLapMaPhieu = quytrinh.ThietLapMaPhieu,
                        GhiChu = quytrinh.GhiChu,
                        NodeMapModels = node,
                        DiagramNodeModels = diagramNode,
                        NhanSuIds = NhanSuIds,
                        PhongBanIds = PhongBanIds,
                        NodeSttings = NodeSttings,
                        NextStepNodeModels = nextstep,
                        NgayBatDau = quytrinh.NgayBatDau,
                        CauHinhThongTins = CauHinhThongTins
                    };
                    return new Response<object>(obj);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
