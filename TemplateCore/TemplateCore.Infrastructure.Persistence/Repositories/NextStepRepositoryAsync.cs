

namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class NextStepRepositoryAsync: GenericRepositoryAsync<NextStep>, INextStepRepositoryAsync
    {
        private readonly DbSet<NextStep> _nextSteps;

        public NextStepRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nextSteps = dbContext.Set<NextStep>();
        }
    }
}
