


namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NextStepRepositoryAsync: GenericRepositoryAsync<NextStep>, INextStepRepositoryAsync
    {
        private readonly DbSet<NextStep> _nextSteps;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public NextStepRepositoryAsync(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUserService ) : base(dbContext)
        {
            _nextSteps = dbContext.Set<NextStep>();
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<IEnumerable<NextStep>> GetbextStepBtQuyTrinhId(int quytrinhid)
        {
            var nextstep = await _nextSteps.Where(x => x.DanhSachQuyTrinhId == quytrinhid && x.UserParentId == _authenticatedUserService.parentUserId).AsQueryable().ToListAsync();

            return nextstep;
        }
    }
}
