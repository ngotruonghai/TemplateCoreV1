
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
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.UserParentId == _authenticatedUserService.parentUserId && x.Status == true).AsNoTracking().ToListAsync();

            return quytrinh;
        }

        /// <summary>
        /// Chỉ đọc , không chỉnh sửa
        /// </summary>
        /// <param name="QuyTrinhId"></param>
        /// <returns></returns>
        public async Task<DanhSachQuyTrinh> GetDanhSachQuyTrinhByid(int QuyTrinhId)
        {
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.UserParentId == _authenticatedUserService.parentUserId 
                                         && x.Id == QuyTrinhId && x.Status == true)
                                        .Include(x => x.NhanSuTheoDoiQuyTrinhs)
                                        .Include(x => x.PhongBanTheoDoiQuyTrinhs)
                                        .Include(x => x.Nodes).ThenInclude(x => x.NodeSettings).ThenInclude(x => x.NhanSuTiepNhanNodes)
                                        .Include(x => x.Nodes).ThenInclude(x => x.NodeSettings).ThenInclude(x => x.PhongBanTiepNhanNodes)
                                        .Include(x => x.DiagramNodes)
                                        .Include(x => x.NextSteps)
                                        .Include(x => x.ThongTinCauHinhs).ThenInclude(x => x.ThongTinCauHinhBuocs)
                                        .FirstOrDefaultAsync();

            return quytrinh;
        }

        public async Task<DanhSachQuyTrinh> GetQuyTrinhId(int QuyTrinhId)
        {
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.Id == QuyTrinhId && x.UserParentId == _authenticatedUserService.parentUserId)
                                                        .AsQueryable()
                                                        .Include(x => x.PhongBanTheoDoiQuyTrinhs)
                                                        .Include(x => x.NhanSuTheoDoiQuyTrinhs)
                                                        .FirstOrDefaultAsync();
            return quytrinh;
        }
    }
}
