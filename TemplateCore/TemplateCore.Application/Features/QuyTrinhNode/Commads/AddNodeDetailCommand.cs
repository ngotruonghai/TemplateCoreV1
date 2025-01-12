using System.ComponentModel.DataAnnotations;
using TemplateCore.Application.Interfaces;
using TemplateCore.Domain.DTOs.QuyTrinh;
using TemplateCore.Domain.Entities.QuyTrinh;

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

            public AddNodeDetailCommandHandler(IDanhSachQuyTrinhRepositoryAsync danhSachQuyTrinhRepositoryAsync,
                IDiagramNodeRepositoryAsync diagramNodeRepository,
                INodeRepositoryAsync nodeRepositoryAsync,
                IMapper mapper)
            {
                _danhSachQuyTrinhRepository = danhSachQuyTrinhRepositoryAsync;
                _diagramNodeRepository = diagramNodeRepository;
                _nodeRepositoryAsync = nodeRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(AddNodeDetailCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var danhsach = _mapper.Map<DanhSachQuyTrinh>(request);
                    await _danhSachQuyTrinhRepository.AddAsync(danhsach);
                    await _danhSachQuyTrinhRepository.SaveChangesAsync();

                    if (request.lsnodes != null && request.lsnodes.Any())
                    {
                        List<Node> lsNode = request.lsnodes.Select(data => new Node
                        {
                            KeyId = data.KeyId,
                            position = data.position,
                            label = data.label,
                            type = data.type,
                            DanhSachQuyTrinhId = danhsach.Id,
                        }).ToList();
                        await _nodeRepositoryAsync.AddRangeAsync(lsNode);
                        await _nodeRepositoryAsync.SaveChangesAsync();
                    }

                    if (request.lsdiagram != null && request.lsdiagram.Any())
                    {
                        List<DiagramNode> lsDiagramNode = request.lsdiagram.Select(data => new DiagramNode
                        {
                            KeyId = data.KeyId,
                            source = data.source,
                            lineAttributes = data.lineAttributes,
                            DanhSachQuyTrinhId = danhsach.Id,
                            target = data.target,
                            
                        }).ToList();
                        await _diagramNodeRepository.AddRangeAsync(lsDiagramNode);
                        await _diagramNodeRepository.SaveChangesAsync();
                    }

                    await _danhSachQuyTrinhRepository.SaveChangesAsync();
                    

                    return new Response<int>(danhsach.Id);
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
