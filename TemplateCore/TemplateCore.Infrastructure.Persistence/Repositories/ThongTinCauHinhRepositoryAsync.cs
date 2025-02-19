
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class ThongTinCauHinhRepositoryAsync: GenericRepositoryAsync<ThongTinCauHinh>, IThongTinCauHinhRepositoryAsync
    {
        private readonly DbSet<ThongTinCauHinh> _thongtincauhinh;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public ThongTinCauHinhRepositoryAsync(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService) : base(dbContext)
        {
            _thongtincauhinh = dbContext.Set<ThongTinCauHinh>();
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<IEnumerable<ThongTinCauHinh>> GetThongTinCauHinhByQuyTrinhId(int quytrinhId)
        {
            var thongtincauhinh = await _thongtincauhinh.Where(x => x.DanhSachQuyTrinhId == quytrinhId && x.UserParentId == _authenticatedUserService.parentUserId)
                                                    .AsQueryable()
                                                    .Include(x => x.ThongTinCauHinhBuocs)
                                                    .ToListAsync();
            return thongtincauhinh;
        }
    }
}
