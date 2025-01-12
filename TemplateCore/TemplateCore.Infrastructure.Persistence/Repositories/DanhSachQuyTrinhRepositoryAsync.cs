
namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class DanhSachQuyTrinhRepositoryAsync : GenericRepositoryAsync<DanhSachQuyTrinh>, IDanhSachQuyTrinhRepositoryAsync
    {
        private readonly DbSet<DanhSachQuyTrinh> _danhSachQuyTrinhs;

        public DanhSachQuyTrinhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _danhSachQuyTrinhs = dbContext.Set<DanhSachQuyTrinh>();
        }

        public async Task<IEnumerable<DanhSachQuyTrinh>> GetQuyTrinhId(int QuyTrinhId)
        {
            var quytrinh = await _danhSachQuyTrinhs.Where(x => x.Id == QuyTrinhId).Include(x => x.Nodes).Include(x => x.DiagramNodes).ToListAsync();
            return quytrinh;
        }
    }
}
