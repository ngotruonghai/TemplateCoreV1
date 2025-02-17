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
                    danhsachquytrinh.TrangThaiQuyTrinh = EnumTrangThaiQuyTrinh.TaoMoi;

                    await _danhSachQuyTrinhRepository.AddAsync(danhsachquytrinh);

                    //CheckValid();

                    List<Node> lsNode = new List<Node>();
                    List<DiagramNode> lsDiagram = new List<DiagramNode>();
                    List<NextStep> lsnextSteps = new List<NextStep>();
                    List<NhanSuTheoDoiQuyTrinh> lsnhansutheodoiquytrinh = new List<NhanSuTheoDoiQuyTrinh>();
                    List<PhongBanTheoDoiQuyTrinh> lsphongbantheodoiquytrinh = new List<PhongBanTheoDoiQuyTrinh>();
                    List<NhanSuTiepNhanNode> lsnhanSuTiepNhanNodes = new List<NhanSuTiepNhanNode>();
                    List<PhongBanTiepNhanNode> lsphongbantiepnhaNodes = new List<PhongBanTiepNhanNode>();
                    List<NodeSetting> lsNodeSetting = new List<NodeSetting>();

                    /* Add Node */
                    foreach (var data in request.NodeMapModels)
                    {
                        lsNode.Add(new Node()
                        {
                            KeyId = data.KeyId,
                            X = data.X,
                            Y = data.Y,
                            Index = data.Index,
                            Type = data.Type,
                            TenNode = data.TenNode,
                            DanhSachQuyTrinhId = danhsachquytrinh.Id
                        });
                    }
                    var node = await _nodeRepositoryAsync.AddRangeAsync(lsNode);

                    /* Add DiagramNode */
                    foreach (var item in request.DiagramNodeModels)
                    {
                        lsDiagram.Add(new DiagramNode()
                        {
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            KeyId = item.KeyId,
                            Source = item.Source,
                            TenDiagram = item.TenDiagram,
                            Target = item.Target,
                        });
                    }
                    await _diagramNodeRepository.AddRangeAsync(lsDiagram);

                    /* Add NextStep */
                    foreach (var item in request.NextStepNodeModels)
                    {
                        lsnextSteps.Add(new NextStep()
                        {
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            NodeIdStart = item.NodeIdStart,
                            NodeIdEnd = item.NodeIdEnd,
                            DiagramId = item.DiagramId,
                            TypeNextStep = item.TypeNextStep,
                            ActionName = item.ActionName,
                            Action = item.Action,

                        });
                    }
                    await _nextStepRepositoryAsync.AddRangeAsync(lsnextSteps);

                    /* Add nhân sự theo dõi */
                    foreach (var data in request.NhanSuIds)
                    {
                        lsnhansutheodoiquytrinh.Add(new NhanSuTheoDoiQuyTrinh()
                        {
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            HoTen = await _accountService.GetUserNameById(data),
                            UserId = data,
                            LoaiNhanSu = EnumCauHinhNhanSuQuyTrinh.NhanSuTheoDoiQuyTrinh,
                        });
                    }
                    await _nhanSuTheoDoiQuyTrinhRepository.AddRangeAsync(lsnhansutheodoiquytrinh);

                    /* Add phòng ban theo dõi */
                    foreach (var data in request.PhongBanIds)
                    {
                        lsphongbantheodoiquytrinh.Add(new PhongBanTheoDoiQuyTrinh()
                        {
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            PhongbanId = data,
                            LoaiPhongBan = EnumCauHinhPhongBanQuyTrinh.PhongBanTheoDoiQuyTrinh,
                            TenPhongBan = await _phongBanRepository.GetNamePhongBanById(data),
                        });
                    }
                    await _phongBanTheoDoiQuyTrinhRepository.AddRangeAsync(lsphongbantheodoiquytrinh);

                    /* Add cấu hình node */
                    foreach (var itemnodesetting in request.NodeSttings)
                    {
                        var nodesetting = await _nodeSettingRepository.AddAsync(new NodeSetting()
                        {
                            NodeId = node.Where(x => x.KeyId == itemnodesetting.KeyNode).Select(x => x.Id).FirstOrDefault(),
                            GhiChu = itemnodesetting.GhiChu,
                            CauHinhMailNhacNho = itemnodesetting.CauHinhMailNhacNho,
                            IsGuiMailNhacNho = itemnodesetting.IsGuiMailNhacNho,
                            IsGuiMailPhongBanTiepNhan = itemnodesetting.IsGuiMailPhongBanTiepNhan,
                            IsTaoTaskBaoCao = itemnodesetting.IsTaoTaskBaoCao,
                        });

                        /* Add nhan sự tiếp nhận node */
                        foreach (var data in itemnodesetting.nhanSuNodeModels)
                        {
                            await _nhanSuTiepNhanNodeRepository.AddAsync(new NhanSuTiepNhanNode()
                            {
                                HoTen = await _accountService.GetUserNameById(data),
                                NodeSettingId = nodesetting.Id,
                                LoaiNhanSu = EnumCauHinhNhanSuQuyTrinh.NhanSuTiepNhanNode,
                                UserId = data
                            });
                        }
                        /* Add phòng ban tiếp nhận node */
                        foreach (var data in itemnodesetting.PhongBanNodeModels)
                        {
                            await _phongBanTiepNhanNodeRepository.AddAsync(new PhongBanTiepNhanNode()
                            {
                                TenPhongBan = await _phongBanRepository.GetNamePhongBanById(data),
                                NodeSettingId = nodesetting.Id,
                                LoaiPhongBan = EnumCauHinhPhongBanQuyTrinh.PhongBanTiepNhanNode,
                                PhongbanId = data
                            });
                        }


                    }

                    /* Add cấu hình bước */
                    foreach (var data_thongtin in request.CauHinhThongTins)
                    {
                        var thongtincauhinh = await _thongtincauhinhRepository.AddAsync(new ThongTinCauHinh()
                        {
                            Index = data_thongtin.Index,
                            IsBatBuocnhap = data_thongtin.IsBatBuocnhap,
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            KichThuocKyTu = data_thongtin.KichThuocKyTu,
                            IsFileDinhKem = data_thongtin.IsFileDinhKem,
                            TenThonTin = data_thongtin.TenThonTin,
                            LoaiThongTin = data_thongtin.LoaiThongTin,
                            NoiDung = data_thongtin.NoiDung,
                            ThongBao = data_thongtin.ThongBao,
                        });
                        foreach (var data in data_thongtin.DanhSachCauHinhBuoc)
                        {
                            await _thongtincauhinhbuocrepository.AddAsync(new ThongTinCauHinhBuoc()
                            {
                                ThongTinCauHinhId = thongtincauhinh.Id,
                                KeyNode = data
                            });
                        }
                    }
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
