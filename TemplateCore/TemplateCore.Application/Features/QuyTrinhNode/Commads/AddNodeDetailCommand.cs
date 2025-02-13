using TemplateCore.Application.Interfaces;
using TemplateCore.Domain.DTOs.QuyTrinh;
using IAuthenticatedUserService = TemplateCore.Application.Interfaces.IAuthenticatedUserService;

namespace TemplateCore.Application.Features.QuyTrinhNode.Commads
{
    public class AddNodeDetailCommand : IRequest<Response<int>>
    {
        public string? MaQuyTrinh { get; set; }
        public string? TenQuyTrinh { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? NoiDung { get; set; }
        public string? ThietLapMaPhieu { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public ICollection<ListNodeModel>? NodeMapModels { get; set; }
        public ICollection<ListDiagramModel>? DiagramNodeModels { get; set; }
        public ICollection<NextStepModel>? NextStepNodeModels { get; set; }
        public ICollection<string>? NhanSuIds { get; set; }
        public ICollection<int>? PhongBanIds { get; set; }
        public ICollection<ListNodeSettingModel>? NodeSttings { get; set; }
        public ICollection<ListCauHingThongTinModel>? CauHinhThongTins { get; set; }

        public class AddNodeDetailCommandHandler : IRequestHandler<AddNodeDetailCommand, Response<int>>
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

            public AddNodeDetailCommandHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync,
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

            public async Task<Response<int>> Handle(AddNodeDetailCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    _transaction.Begin();
                    DateTime now = DateTime.Now;
                    request.UserId = _authenticatedUserService.UserId;
                    request.MaQuyTrinh = "QT" + now.ToString("yyMMddHHmmss");

                    var danhsachquytrinh = _mapper.Map<DanhSachQuyTrinh>(request);

                    #region khai báo biến
                    List<NhanSuTheoDoiQuyTrinh> lsNhanSuTheoDoiQuyTring = new List<NhanSuTheoDoiQuyTrinh>();
                    List<PhongBanTheoDoiQuyTrinh> lsPhongBanTheoDoiQuyTrinh = new List<PhongBanTheoDoiQuyTrinh>();
                    List<NextStep> lsNextStep = new List<NextStep>();
                    List<DiagramNode> lsDiagram = new List<DiagramNode>();
                    List<NhanSuTiepNhanNode> lsNhanSuTiepNhanNode = new List<NhanSuTiepNhanNode>();
                    List<Node> lsNode = new List<Node>();
                    List<NhanSuTiepNhanNode> lsnhansutieonhannode = new List<NhanSuTiepNhanNode>();
                    List<PhongBanTiepNhanNode> lsphongbantiepnhannode = new List<PhongBanTiepNhanNode>();
                    List<NodeSetting> nodeST = new List<NodeSetting>();
                    List<ThongTinCauHinh> lsThongTinCauHinh = new List<ThongTinCauHinh>();
                    List<ThongTinCauHinhBuoc> lsThongTinCauHingBuoc = new List<ThongTinCauHinhBuoc>();

                    #endregion


                    /* Add Nhân sự theo dõi quy trình */
                    foreach (var data in request.NhanSuIds)
                    {
                        lsNhanSuTheoDoiQuyTring.Add(new NhanSuTheoDoiQuyTrinh()
                        {
                            HoTen = await _accountService.GetUserNameById(data),
                            UserId = data,
                            LoaiNhanSu = EnumCauHinhNhanSuQuyTrinh.NhanSuTheoDoiQuyTrinh,
                            Created = DateTime.Now,
                        });
                    }
                    danhsachquytrinh.NhanSuTheoDoiQuyTrinhs = lsNhanSuTheoDoiQuyTring;

                    /* Add phòng ban theo dõi quy trình */
                    foreach (var data in request.PhongBanIds)
                    {
                        lsPhongBanTheoDoiQuyTrinh.Add(new PhongBanTheoDoiQuyTrinh()
                        {
                            PhongbanId = data,
                            LoaiPhongBan = EnumCauHinhPhongBanQuyTrinh.PhongBanTheoDoiQuyTrinh,
                            TenPhongBan = await _phongBanRepository.GetNamePhongBanById(data),
                        });
                    }
                    danhsachquytrinh.PhongBanTheoDoiQuyTrinhs = lsPhongBanTheoDoiQuyTrinh;


                    /* Add DiagramNodes */
                    foreach (var data in request.DiagramNodeModels)
                    {
                        lsDiagram.Add(new DiagramNode()
                        {
                            KeyId = data.KeyId,
                            Source = data.Source,
                            Target = data.Target,
                        });
                    }
                    danhsachquytrinh.DiagramNodes = lsDiagram;

                    /* Add NextSteps */
                    foreach (var data in request.NextStepNodeModels)
                    {
                        lsNextStep.Add(new NextStep()
                        {
                            NodeIdStart = data.NodeIdStart,
                            NodeIdEnd = data.NodeIdEnd,
                            DiagramId = data.DiagramId,
                            TypeNextStep = data.TypeNextStep,
                            ActionName = data.ActionName,
                            Action = data.Action,
                        });
                    }
                    danhsachquytrinh.NextSteps = lsNextStep;
                    /* Add node => NodeSetting */
                    foreach (var node in request.NodeMapModels)
                    {
                        var lsnodesetting = request.NodeSttings.FirstOrDefault(x => x.KeyNode == node.KeyId);

                        foreach (var data in lsnodesetting.nhanSuNodeModels)
                        {
                            lsnhansutieonhannode.Add(new NhanSuTiepNhanNode()
                            {
                                HoTen = await _accountService.GetUserNameById(data),
                                LoaiNhanSu = EnumCauHinhNhanSuQuyTrinh.NhanSuTiepNhanNode,
                                UserId = data
                            });
                        }

                        foreach (var data in lsnodesetting.PhongBanNodeModels)
                        {
                            lsphongbantiepnhannode.Add(new PhongBanTiepNhanNode()
                            {
                                TenPhongBan = await _phongBanRepository.GetNamePhongBanById(data),
                                LoaiPhongBan = EnumCauHinhPhongBanQuyTrinh.PhongBanTiepNhanNode,
                                PhongbanId = data
                            });
                        }
                        nodeST.Add(new NodeSetting()
                        {
                            GhiChu = lsnodesetting.GhiChu,
                            CauHinhMailNhacNho = lsnodesetting.CauHinhMailNhacNho,
                            IsGuiMailNhacNho = lsnodesetting.IsGuiMailNhacNho,
                            IsGuiMailPhongBanTiepNhan = lsnodesetting.IsGuiMailPhongBanTiepNhan,
                            IsTaoTaskBaoCao = lsnodesetting.IsTaoTaskBaoCao,
                            NhanSuTiepNhanNodes = lsnhansutieonhannode,
                            PhongBanTiepNhanNodes = lsphongbantiepnhannode
                        });

                        lsNode.Add(new Node()
                        {
                            KeyId = node.KeyId,
                            X = node.X,
                            Y = node.Y,
                            Index = node.Index,
                            Type = node.Type,
                            TenNode = node.TenNode,
                            NodeSettings = nodeST
                        });
                    }
                    danhsachquytrinh.Nodes = lsNode;

                    foreach (var data in request.CauHinhThongTins)
                    {
                        foreach (var buoc in data.DanhSachCauHinhBuoc)
                        {
                            lsThongTinCauHingBuoc.Add(new ThongTinCauHinhBuoc()
                            {
                                KeyNode = buoc,
                            });
                        }

                        lsThongTinCauHinh.Add(new ThongTinCauHinh()
                        {
                            Index = data.Index,
                            IsBatBuocnhap = data.IsBatBuocnhap,
                            IsFileDinhKem = data.IsFileDinhKem,
                            TenThonTin = data.TenThonTin,
                            KichThuocKyTu = data.KichThuocKyTu,
                            NoiDung = data.NoiDung,
                            LoaiThongTin = data.LoaiThongTin,
                            ThongBao = data.ThongBao,
                            ThongTinCauHinhBuocs = lsThongTinCauHingBuoc
                        });
                    }
                    danhsachquytrinh.ThongTinCauHinhs = lsThongTinCauHinh;

                    await _danhSachQuyTrinhRepository.AddAsync(danhsachquytrinh);

                    _transaction.Commit();

                    return new Response<int>(danhsachquytrinh.Id);

                    #region local function
                    void CheckValid()
                    {
                        if (request.NodeMapModels.Any() == false) throw new Exception("Bạn chưa tạo bước trong quy trình");
                        if (request.DiagramNodeModels.Any() == false) throw new Exception("Bạn chưa tạo bước trong quy trình");
                        if (request.NextStepNodeModels.Any() == false) throw new Exception("Bạn chưa tạo bước trong quy trình");
                        if (request.PhongBanIds.Any() == false) throw new Exception("Chưa chọn phòng ban theo dõi");
                        if (request.NhanSuIds.Any() == false) throw new Exception("Chưa chọn nhân sự theo dõi");
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    // Log lỗi và xử lý nếu cần
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}
