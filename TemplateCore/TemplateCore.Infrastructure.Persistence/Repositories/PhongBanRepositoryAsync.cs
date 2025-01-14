
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class PhongBanRepositoryAsync : GenericRepositoryAsync<PhongBan>,IPhongBanRepositoryAsync
    {
        private readonly DbSet<PhongBan> _phongban;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public PhongBanRepositoryAsync(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService) : base(dbContext)
        {
            _phongban = dbContext.Set<PhongBan>();
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<IEnumerable<PhongBan>> DanhSachPhongBan()
        {
            if(_authenticatedUserService.CheckUserSupperAdmin == true) return await _phongban.ToListAsync();

            else return await _phongban.Where(x => x.UserParentId == _authenticatedUserService.parentUserId).ToListAsync();
        }

        public async Task<PhongBan> GetPhongBanId(int Id)
        {
            string parentUserId = _authenticatedUserService.parentUserId;
            if(_authenticatedUserService.CheckUserSupperAdmin == true)
            {
                return await _phongban.Where(x => x.Id == Id).FirstOrDefaultAsync();
            }
            else
            {
                return await _phongban.Where(x => x.Id == Id && x.UserParentId == parentUserId).FirstOrDefaultAsync();
            }
        }
    }
}
