using TemplateCore.Domain.DTOs.QuyTrinh;

namespace TemplateCore.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Nhân sự xử lý
            CreateMap<AddNodeDetailCommand, DanhSachQuyTrinh>();
            CreateMap<ListDiagramModel, DiagramNode>();
            CreateMap<Node, ListNodeModel>();
            #endregion
        }
    }
}
