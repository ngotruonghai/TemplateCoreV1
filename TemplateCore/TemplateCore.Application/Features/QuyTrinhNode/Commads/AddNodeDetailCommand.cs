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

        public List<ListNodeModel>? lsnodes { get; set; }
        public List<ListDiagramModel>? lsdiagram { get; set; }

        public class AddNodeDetailCommandHandler : IRequestHandler<AddNodeDetailCommand, Response<int>>
        {
            private readonly IDanhSachQuyTrinhRepositoryAsync _danhSachQuyTrinhRepository;
            private readonly IDiagramNodeRepositoryAsync _diagramNodeRepository;
            private readonly INodeRepositoryAsync _nodeRepositoryAsync;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUserService _authenticatedUserService;

            public AddNodeDetailCommandHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync,
                IDiagramNodeRepositoryAsync diagramNodeRepository,
                INodeRepositoryAsync nodeRepositoryAsync,
                IMapper mapper,
                IAuthenticatedUserService authenticatedUserService,
                ITransaction transaction)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepositoryAsync;
                _diagramNodeRepository = diagramNodeRepository;
                _nodeRepositoryAsync = nodeRepositoryAsync;
                _mapper = mapper;
                _authenticatedUserService = authenticatedUserService;
            }

            public async Task<Response<int>> Handle(AddNodeDetailCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Response<int>();
                }
                catch (Exception ex)
                {
                    // Log lỗi và xử lý nếu cần
                    throw new Exception("Có lỗi xảy ra khi xử lý dữ liệu.", ex);
                }

            }
        }
    }
}
