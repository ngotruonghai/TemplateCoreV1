
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DanhSachQuyTrinhRepositoryAsync : GenericRepositoryAsync<DanhSachQuyTrinh>, IDanhSachQuyTrinhRepositoryAsync
    {
        private readonly DbSet<DanhSachQuyTrinh> _danhSachQuyTrinhs;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public DanhSachQuyTrinhRepositoryAsync(ApplicationDbContext dbContext
            , IAuthenticatedUserService authenticatedUserService) : base(dbContext)
        {
            _danhSachQuyTrinhs = dbContext.Set<DanhSachQuyTrinh>();
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<IEnumerable<DanhSachQuyTrinh>> GetAllDanhSachQuyTrinh()
        {
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.UserParentId == _authenticatedUserService.parentUserId).ToListAsync();

            return quytrinh;
        }

        public async Task<IEnumerable<DanhSachQuyTrinh>> GetQuyTrinhId(int QuyTrinhId)
        {
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.Id == QuyTrinhId).Include(x => x.Nodes).Include(x => x.DiagramNodes).ToListAsync();
            return quytrinh;
        }
    }
}
