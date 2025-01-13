namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class Transaction : ITransaction
    {
        private readonly ApplicationDbContext _dbContext;

        public Transaction(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Begin()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
