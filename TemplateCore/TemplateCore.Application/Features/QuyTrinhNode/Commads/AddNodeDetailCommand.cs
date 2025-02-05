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

        public string? NoiDung {  get; set; }

        public string? ThietLapMaPhieu {  get; set; }

        public string? GhiChu {  get; set; }

        public DateTime? NgayBatDau {  get; set; }
        public ICollection<ListNodeModel>? NodeMapModels { get; set; }
        public ICollection<ListDiagramModel>? DiagramNodeModels { get; set; }
        public ICollection<NextStepModel>? NextStepNodeModels { get; set; }
        public ICollection<string>? NhanSuIds { get; set; }
        public ICollection<int>? PhongBanIds { get; set; }
        public ICollection<ListNodeSettingModel>? NodeSttings { get; set; }

        public class AddNodeDetailCommandHandler : IRequestHandler<AddNodeDetailCommand, Response<int>>
        {
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
                IPhongBanRepositoryAsync phongBanRepository)
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
                    await _danhSachQuyTrinhRepository.AddAsync(danhsachquytrinh);

                    CheckValid();

                    List<Node> lsNode = new List<Node>();
                    List<DiagramNode> lsDiagram = new List<DiagramNode>();
                    List<NextStep> lsnextSteps = new List<NextStep>();
                    List<NhanSuTheoDoiQuyTrinh> lsnhansutheodoiquytrinh = new List<NhanSuTheoDoiQuyTrinh>();
                    List<PhongBanTheoDoiQuyTrinh> lsphongbantheodoiquytrinh = new List<PhongBanTheoDoiQuyTrinh>();

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
                    await _nodeRepositoryAsync.AddRangeAsync(lsNode);

                    foreach (var item in request.DiagramNodeModels)
                    {
                        lsDiagram.Add(new DiagramNode()
                        {
                            DanhSachQuyTrinhId = danhsachquytrinh.Id,
                            KeyId = item.KeyId,
                            Source = item.Source,
                            Target = item.Target,
                        });
                    }
                    await _diagramNodeRepository.AddRangeAsync(lsDiagram);

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

                    foreach(var data in request.NhanSuIds)
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

                    foreach(var data in request.PhongBanIds)
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
