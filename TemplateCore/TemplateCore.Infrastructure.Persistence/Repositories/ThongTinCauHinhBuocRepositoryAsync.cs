using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCore.Infrastructure.Persistence.Repositories
{
    public class ThongTinCauHinhBuocRepositoryAsync : GenericRepositoryAsync<ThongTinCauHinhBuoc>, IThongTinCauHinhBuocRepositoryAsync
    {
        private readonly DbSet<ThongTinCauHinhBuoc> _thongtincauhinhbuoc;

        public ThongTinCauHinhBuocRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _thongtincauhinhbuoc = dbContext.Set<ThongTinCauHinhBuoc>();
        }
    }
}
